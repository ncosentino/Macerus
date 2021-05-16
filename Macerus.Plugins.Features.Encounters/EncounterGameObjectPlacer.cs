using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.Combat.Api;
using Macerus.Plugins.Features.Stats;

using NexusLabs.Framework;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Logging;
using ProjectXyz.Plugins.Features.Mapping.Api;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

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
            var spawnPositionEntries = _mapGameObjectManager
                .GameObjects
                .Select(x => x.Get<IEncounterSpawnLocationBehavior>().FirstOrDefault())
                .Where(x => x != null)
                .Select(x => new
                {
                    AllowedTeams = x.AllowedTeams,
                    Position = x.Owner.GetOnly<IReadOnlyPositionBehavior>()
                })
                .ToList();
            foreach (var gameObjectToPlace in gameObjectsToPlace)
            {
                var teamId = (int)_statCalculationServiceAmenity.GetStatValue(
                    gameObjectToPlace,
                    _combatTeamIdentifiers.CombatTeamStatDefinitionId);

                var applicableSpawnLocationEntries = spawnPositionEntries
                    .Where(x => x.AllowedTeams.Contains(teamId))
                    .ToArray();
                var selectedSpawnEntry = applicableSpawnLocationEntries[_random.Next(0, applicableSpawnLocationEntries.Length)];
                var spawnPosition = selectedSpawnEntry.Position;

                var gameObjectPosition = gameObjectToPlace.GetOnly<IPositionBehavior>();
                gameObjectPosition.SetPosition(spawnPosition.X, spawnPosition.Y);
                _logger.Debug(
                    $"Set position of '{gameObjectToPlace}' to " +
                    $"({gameObjectPosition.X},{gameObjectPosition.Y}).");

                spawnPositionEntries.Remove(selectedSpawnEntry);
                _mapGameObjectManager.MarkForRemoval(spawnPosition.Owner);
                _mapGameObjectManager.MarkForAddition(gameObjectToPlace);
            }

            // remove remaining spawn locations
            foreach (var spawnPositionEntry in spawnPositionEntries)
            {
                _mapGameObjectManager.MarkForRemoval(spawnPositionEntry.Position.Owner);
            }

            // force synchronization so later handlers can operate
            _mapGameObjectManager.Synchronize();
        }
    }
}
