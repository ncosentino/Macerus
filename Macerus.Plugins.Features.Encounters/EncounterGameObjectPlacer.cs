using System.Collections.Generic;
using System.Linq;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.Combat.Api;
using Macerus.Plugins.Features.Stats;

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
        private readonly IStatCalculationServiceAmenity _statCalculationServiceAmenity;
        private readonly ICombatTeamIdentifiers _combatTeamIdentifiers;
        private readonly ILogger _logger;
        private readonly IRandom _random;

        public EncounterGameObjectPlacer(
            IRandom random,
            IMapGameObjectManager mapGameObjectManager,
            IStatCalculationServiceAmenity statCalculationServiceAmenity,
            ICombatTeamIdentifiers combatTeamIdentifiers, 
            ILogger logger)
        {
            _random = random;
            _mapGameObjectManager = mapGameObjectManager;
            _statCalculationServiceAmenity = statCalculationServiceAmenity;
            _combatTeamIdentifiers = combatTeamIdentifiers;
            _logger = logger;
        }

        public void PlaceGameObjects(IEnumerable<IGameObject> gameObjectsToPlace)
        {
            var spawnLocationEntries = _mapGameObjectManager
                .GameObjects
                .Select(x => x.Get<IEncounterSpawnLocationBehavior>().FirstOrDefault())
                .Where(x => x != null)
                .Select(x => new
                {
                    AllowedTeams = x.AllowedTeams,
                    WorldLocation = x.Owner.GetOnly<IReadOnlyWorldLocationBehavior>()
                })
                .ToList();
            foreach (var gameObjectToPlace in gameObjectsToPlace)
            {
                var teamId = (int)_statCalculationServiceAmenity.GetStatValue(
                    gameObjectToPlace,
                    _combatTeamIdentifiers.CombatTeamStatDefinitionId);

                var applicableSpawnLocationEntries = spawnLocationEntries
                    .Where(x => x.AllowedTeams.Contains(teamId))
                    .ToArray();
                var selectedSpawnEntry = applicableSpawnLocationEntries[_random.Next(0, applicableSpawnLocationEntries.Length)];
                var spawnLocation = selectedSpawnEntry.WorldLocation;

                var gameObjectLocation = gameObjectToPlace.GetOnly<IWorldLocationBehavior>();
                gameObjectLocation.SetLocation(spawnLocation.X, spawnLocation.Y);
                _logger.Debug(
                    $"Set location of '{gameObjectToPlace}' to " +
                    $"({gameObjectLocation.X},{gameObjectLocation.Y}).");

                spawnLocationEntries.Remove(selectedSpawnEntry);
                _mapGameObjectManager.MarkForRemoval((IGameObject)spawnLocation.Owner); // FIXME: whyyy these casts
                _mapGameObjectManager.MarkForAddition(gameObjectToPlace);
            }

            // remove remaining spawn locations
            foreach (var spawnLocationEntry in spawnLocationEntries)
            {
                _mapGameObjectManager.MarkForRemoval((IGameObject)spawnLocationEntry.WorldLocation.Owner); // FIXME: whyyy these casts
            }

            // force synchronization so later handlers can operate
            _mapGameObjectManager.Synchronize();
        }
    }
}
