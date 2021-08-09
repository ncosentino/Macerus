using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Macerus.Plugins.Features.Encounters.EndHandlers;
using Macerus.Plugins.Features.GameObjects.Items.Generation.Api;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.Filtering.Api;

namespace Macerus.Plugins.Features.Encounters.Default.EndHandlers
{
    public sealed class CombatGenerateRewardsEndEncounterHandler : IDiscoverableEndEncounterHandler
    {
        private readonly Lazy<ILootGeneratorAmenity> _lazyLootGeneratorAmenity;
        private readonly IGameObjectFactory _gameObjectFactory;

        public CombatGenerateRewardsEndEncounterHandler(
            Lazy<ILootGeneratorAmenity> lazyLootGeneratorAmenity,
            IGameObjectFactory gameObjectFactory)
        {
            _lazyLootGeneratorAmenity = lazyLootGeneratorAmenity;
            _gameObjectFactory = gameObjectFactory;
        }

        public async Task<IGameObject> HandleAsync(
            IGameObject encounter,
            IFilterContext filterContext)
        {
            if (!encounter.TryGetFirst<IEncounterCombatOutcomeBehavior>(out var combatOutcomeBehavior))
            {
                return encounter;
            }

            if (!combatOutcomeBehavior.WinningTeam.Any())
            {
                return encounter;
            }

            if (!encounter.TryGetFirst<IEncounterGenerateCombatRewardsBehavior>(out var generateCombatRewardsBehavior))
            {
                return encounter;
            }

            var loot = generateCombatRewardsBehavior.DropTableId == null
                ? Array.Empty<IGameObject>()
                : _lazyLootGeneratorAmenity
                    .Value
                    .GenerateLoot(
                        generateCombatRewardsBehavior.DropTableId,
                        filterContext)
                    .ToArray();
            var combatRewardsBehavior = new EncounterCombatRewardsBehavior(
                loot,
                generateCombatRewardsBehavior.Experience);
            encounter = _gameObjectFactory.Create(encounter
                .Behaviors
                .Concat(new IBehavior[]
                {
                    combatRewardsBehavior
                }));
            return encounter;
        }
    }
}
