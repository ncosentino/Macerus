using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Combat.Api;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Plugins.Features.Mapping;

namespace Macerus.Plugins.Features.Combat.Default
{
    public sealed class CombatGameObjectProvider : ICombatGameObjectProvider
    {
        private readonly IMapGameObjectManager _mapGameObjectManager;
        private readonly IActorIdentifiers _actorIdentifiers;
        private readonly List<IGameObject> _cachedGameObjects;
        private bool _dirtyCache;

        public CombatGameObjectProvider(
            IMapGameObjectManager mapGameObjectManager,
            IActorIdentifiers actorIdentifiers)
        {
            _dirtyCache = true;
            _cachedGameObjects = new List<IGameObject>();
            _mapGameObjectManager = mapGameObjectManager;
            _actorIdentifiers = actorIdentifiers;
            _mapGameObjectManager.Synchronized += MapGameObjectManager_Synchronized;
        }

        private void MapGameObjectManager_Synchronized(object sender, GameObjectsSynchronizedEventArgs e)
        {
            _dirtyCache = true;
        }

        public IEnumerable<IGameObject> GetGameObjects()
        {
            if (_dirtyCache)
            {
                _cachedGameObjects.Clear();
                _cachedGameObjects.AddRange(_mapGameObjectManager
                    .GameObjects
                    .Where(x =>
                    {
                        if (!x.TryGetFirst<ITypeIdentifierBehavior>(out var typeIdentifierBehavior))
                        {
                            return false;
                        }

                        return typeIdentifierBehavior.TypeId.Equals(_actorIdentifiers.ActorTypeIdentifier);
                    }));
                _dirtyCache = false;
            }
            
            return _cachedGameObjects;
        }
    }
}
