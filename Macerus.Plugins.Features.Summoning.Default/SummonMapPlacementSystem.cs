using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Macerus.Plugins.Features.GameObjects.Actors;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Systems;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Mapping;

namespace Macerus.Plugins.Features.Summoning.Default
{
    public sealed class SummonMapPlacementSystem : IDiscoverableSystem
    {
        private readonly Lazy<IMapGameObjectManager> _lazyMapGameObjectManager;
        private readonly Lazy<IMacerusActorIdentifiers> _lazyMacerusActorIdentifiers;
        private readonly Dictionary<IGameObject, ISummoningBehavior> _summoners;

        public SummonMapPlacementSystem(
            Lazy<IMapGameObjectManager> lazyMapGameObjectManager,
            Lazy<IMacerusActorIdentifiers> lazyMacerusActorIdentifiers)
        {
            _lazyMapGameObjectManager = lazyMapGameObjectManager;
            _lazyMacerusActorIdentifiers = lazyMacerusActorIdentifiers;

            _summoners = new Dictionary<IGameObject, ISummoningBehavior>();

            // FIXME: no point in lazy because of this?
            _lazyMapGameObjectManager.Value.Synchronized += MapGameObjectManager_Synchronized;
        }

        public int? Priority => null;

        public async Task UpdateAsync(ISystemUpdateContext systemUpdateContext)
        {
        }

        private void MapGameObjectManager_Synchronized(
           object sender,
           GameObjectsSynchronizedEventArgs e)
        {
            foreach (var summonerEntry in e
                .Added
                .Where(x =>
                    x.TryGetFirst<ITypeIdentifierBehavior>(out var typeIdentifierBehavior) &&
                    Equals(typeIdentifierBehavior.TypeId, _lazyMacerusActorIdentifiers.Value.ActorTypeIdentifier))
                .Select(x => new
                {
                    Actor = x,
                    SummoningBehavior = x.TryGetFirst<ISummoningBehavior>(out var summoningBehavior)
                        ? summoningBehavior
                        : null,
                })
                .Where(x => x.SummoningBehavior != null))
            {
                _summoners[summonerEntry.Actor] = summonerEntry.SummoningBehavior;
                summonerEntry.SummoningBehavior.Summoned += SummoningBehavior_Summoned;
                summonerEntry.SummoningBehavior.Unsummoned += SummoningBehavior_Unsummoned;
            }

            foreach (var gameObject in e.Removed)
            {
                if (!_summoners.TryGetValue(
                    gameObject,
                    out var summoningBehavior))
                {
                    continue;
                }

                summoningBehavior.Summoned -= SummoningBehavior_Summoned;
                summoningBehavior.Unsummoned -= SummoningBehavior_Unsummoned;
            }
        }

        private void SummoningBehavior_Unsummoned(
            object sender,
            SummonEventArgs e)
        {
            _lazyMapGameObjectManager.Value.MarkForRemoval(e.Summons);
        }

        private void SummoningBehavior_Summoned(
            object sender,
            SummonEventArgs e)
        {
            _lazyMapGameObjectManager.Value.MarkForAddition(e.Summons);
            // FIXME: where do these need to get placed?
        }
    }
}
