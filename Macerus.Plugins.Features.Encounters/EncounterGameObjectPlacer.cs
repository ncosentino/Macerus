using System.Collections.Generic;
using System.Linq;

using Macerus.Api.Behaviors;

using NexusLabs.Framework;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Logging;
using ProjectXyz.Plugins.Features.Mapping.Api;

namespace Macerus.Plugins.Features.Encounters
{
    public sealed class EncounterGameObjectPlacer : IEncounterGameObjectPlacer
    {
        private readonly IMapGameObjectManager _mapGameObjectManager;
        private readonly ILogger _logger;
        private readonly IRandom _random;

        public EncounterGameObjectPlacer(
            IRandom random,
            IMapGameObjectManager mapGameObjectManager,
            ILogger logger)
        {
            _random = random;
            _mapGameObjectManager = mapGameObjectManager;
            _logger = logger;
        }

        public void PlaceGameObjects(IEnumerable<IGameObject> gameObjectsToPlace)
        {
            // FIXME: figure out how to do placement of actors onto the map in
            // like... starting positions
            var availableSpawnLocations = _mapGameObjectManager
                .GameObjects
                .Where(x => x.Has<IEncounterSpawnLocationBehavior>())
                .Select(x => x.GetOnly<IReadOnlyWorldLocationBehavior>())
                .ToList();
            foreach (var gameObjectToPlace in gameObjectsToPlace)
            {
                // FIXME: instead of being ENTIRELY random it would be great
                // to have some mechanism to figure out what "teams" or types
                // of objects can spawn at particular locations. this was left
                // generic to be able to potentially place other random stuff
                // on the map and not necessarily actors only.
                var spawnLocation = availableSpawnLocations[_random.Next(0, availableSpawnLocations.Count)];
                var gameObjectLocation = gameObjectToPlace.GetOnly<IWorldLocationBehavior>();
                gameObjectLocation.SetLocation(spawnLocation.X, spawnLocation.Y);
                _logger.Debug(
                    $"Set location of '{gameObjectToPlace}' to " +
                    $"({gameObjectLocation.X},{gameObjectLocation.Y}).");

                availableSpawnLocations.Remove(spawnLocation);
                _mapGameObjectManager.MarkForRemoval((IGameObject)spawnLocation.Owner); // FIXME: whyyy these casts
                _mapGameObjectManager.MarkForAddition(gameObjectToPlace);
            }

            // force synchronization so later handlers can operate
            _mapGameObjectManager.Synchronize();
        }
    }
}
