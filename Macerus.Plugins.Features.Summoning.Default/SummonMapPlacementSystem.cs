using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

using Macerus.Plugins.Features.GameObjects.Actors;
using Macerus.Plugins.Features.GameObjects.Skills;

using NexusLabs.Framework;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Logging;
using ProjectXyz.Api.Systems;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Mapping;

namespace Macerus.Plugins.Features.Summoning.Default
{
    public sealed class SummonMapPlacementSystem : IDiscoverableSystem
    {
        private readonly Lazy<IMapGameObjectManager> _lazyMapGameObjectManager;
        private readonly Lazy<ISkillTargetingAmenity> _lazySkillTargetingAmenity;
        private readonly Lazy<IMacerusActorIdentifiers> _lazyMacerusActorIdentifiers;
        private readonly IRandom _random;
        private readonly ILogger _logger;
        private readonly Dictionary<IGameObject, ISummoningBehavior> _summoners;

        public SummonMapPlacementSystem(
            Lazy<IMapGameObjectManager> lazyMapGameObjectManager,
            Lazy<ISkillTargetingAmenity> lazySkillTargetingAmenity,
            Lazy<IMacerusActorIdentifiers> lazyMacerusActorIdentifiers,
            IRandom random,
            ILogger logger)
        {
            _lazyMapGameObjectManager = lazyMapGameObjectManager;
            _lazySkillTargetingAmenity = lazySkillTargetingAmenity;
            _lazyMacerusActorIdentifiers = lazyMacerusActorIdentifiers;
            _random = random;
            _logger = logger;
            _summoners = new Dictionary<IGameObject, ISummoningBehavior>();

            // FIXME: no point in lazy because of this?
            _lazyMapGameObjectManager.Value.Synchronized += MapGameObjectManager_Synchronized;
        }

        public int? Priority => null;

        public async Task UpdateAsync(ISystemUpdateContext systemUpdateContext)
        {
        }

        private void PositionSummon(
            IGameObject summoner, 
            IGameObject summon,
            ISummonTargetLocationBehavior summonTargetLocationBehavior)
        {
            _logger.Debug(
                $"Positioning summon '{summon}' for '{summoner}'...");

            var summonablePositions = _lazySkillTargetingAmenity
                .Value
                .GetUnobstructedTilePositions(summonTargetLocationBehavior.Locations)
                .ToArray();
            if (!summonablePositions.Any())
            {
                throw new InvalidOperationException(
                    "// FIXME: There's no free space around the summoner!");
            }

            var positionIndex = _random.Next(0, summonablePositions.Length);
            var chosenPosition = summonablePositions[positionIndex];
            var gameObjectPosition = summon.GetOnly<IPositionBehavior>();
            gameObjectPosition.SetPosition(chosenPosition);

            _logger.Debug(
                $"Positioned summon '{summon}' for '{summoner}' at " +
                $"({chosenPosition.X},{chosenPosition.Y}).");
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
                summonerEntry.SummoningBehavior.SummonedAsync += SummoningBehavior_Summoned;
                summonerEntry.SummoningBehavior.UnsummonedAsync += SummoningBehavior_Unsummoned;
            }

            foreach (var gameObject in e.Removed)
            {
                if (!_summoners.TryGetValue(
                    gameObject,
                    out var summoningBehavior))
                {
                    continue;
                }

                summoningBehavior.SummonedAsync -= SummoningBehavior_Summoned;
                summoningBehavior.UnsummonedAsync -= SummoningBehavior_Unsummoned;
            }
        }

        private async void SummoningBehavior_Unsummoned(
            object sender,
            SummonEventArgs e)
        {
            _logger.Debug(
                $"Unsummoning {e.Summons.Count} summons for '{((IBehavior)sender).Owner}'...");
            _lazyMapGameObjectManager
                .Value
                .MarkForRemoval(e.Summons);
            await _lazyMapGameObjectManager
                .Value
                .SynchronizeAsync()
                .ConfigureAwait(false);
            _logger.Debug(
                $"Unsummoned {e.Summons.Count} summons for '{((IBehavior)sender).Owner}'.");
        }

        private async void SummoningBehavior_Summoned(
            object sender,
            SummonEventArgs e)
        {
            var summoner = ((IBehavior)sender).Owner;
            var summonTargetLocationBehavior = e
                .SummoningEnchantment
                .GetOnly<ISummonTargetLocationBehavior>();

            _logger.Debug(
                $"Summoning {e.Summons.Count} summons for '{summoner}'...");
            _lazyMapGameObjectManager.Value.MarkForAddition(e.Summons);
           
            foreach (var summon in e.Summons)
            {
                PositionSummon(summoner, summon, summonTargetLocationBehavior);
            }

            await _lazyMapGameObjectManager
                .Value
                .SynchronizeAsync()
                .ConfigureAwait(false);
            _logger.Debug(
                $"Summoned {e.Summons.Count} summons for '{summoner}'.");
        }
    }
}
