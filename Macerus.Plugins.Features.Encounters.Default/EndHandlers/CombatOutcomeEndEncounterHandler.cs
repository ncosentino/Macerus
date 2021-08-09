using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Macerus.Plugins.Features.Encounters.EndHandlers;
using Macerus.Plugins.Features.GameObjects.Actors;
using Macerus.Plugins.Features.GameObjects.Items.Generation.Api;
using Macerus.Plugins.Features.Gui;

using NexusLabs.Contracts;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.PartyManagement;

namespace Macerus.Plugins.Features.Encounters.Default.EndHandlers
{
    public sealed class CombatOutcomeEndEncounterHandler : IDiscoverableEndEncounterHandler
    {
        private readonly Lazy<IModalManager> _lazyModalManager;
        private readonly Lazy<ILootGeneratorAmenity> _lazyLootGeneratorAmenity;
        private readonly Lazy<IRosterManager> _lazyRosterManager;
        private readonly IMacerusActorIdentifiers _macerusActorIdentifiers;

        public CombatOutcomeEndEncounterHandler(
            Lazy<IModalManager> lazyModalManager,
            Lazy<ILootGeneratorAmenity> lazyLootGeneratorAmenity,
            Lazy<IRosterManager> lazyRosterManager,
            IMacerusActorIdentifiers macerusActorIdentifiers)
        {
            _lazyModalManager = lazyModalManager;
            _lazyLootGeneratorAmenity = lazyLootGeneratorAmenity;
            _lazyRosterManager = lazyRosterManager;
            _macerusActorIdentifiers = macerusActorIdentifiers;
        }

        public async Task HandleAsync(
            IGameObject encounter,
            IFilterContext filterContext)
        {
            if (!encounter.TryGetFirst<IEncounterCombatOutcomeBehavior>(out var combatOutcomeBehavior))
            {
                return;
            }

            if (!combatOutcomeBehavior.WinningTeam.Any())
            {
                return;
            }

            var playerWon = combatOutcomeBehavior.PlayerWon == true;

            IReadOnlyCollection<IGameObject> encounterWonItems = new IGameObject[0];
            if (encounter.TryGetFirst<IEncounterCombatRewardsBehavior>(out var combatRewardsBehavior))
            {
                encounterWonItems = combatRewardsBehavior.DropTableId == null
                    ? Array.Empty<IGameObject>()
                    : _lazyLootGeneratorAmenity
                        .Value
                        .GenerateLoot(
                            combatRewardsBehavior.DropTableId,
                            filterContext)
                        .ToArray();
                var inventory = _lazyRosterManager
                    .Value
                    .ActivePartyLeader
                    ?.Get<IItemContainerBehavior>()
                    ?.SingleOrDefault(x => x.ContainerId.Equals(_macerusActorIdentifiers.InventoryIdentifier));
                Contract.RequiresNotNull(
                    inventory,
                    $"There was no inventory found to add the encounter rewards to.");
                foreach (var item in encounterWonItems)
                {
                    Contract.Requires(
                        inventory.TryAddItem(item),
                        $"Could not add encounter reward '{item}' to inventory '{inventory}'. " +
                        $"// FIXME: we need a forcing function to add items to inventories.");
                }
            }

            if (playerWon)
            {
                await _lazyModalManager
                    .Value
                    .ShowAndWaitMessageBoxAsync(
                        $"You won the fight!\r\n" +
                        $"{encounterWonItems.Count} items were added to your inventory.\r\n" +
                        $"// FIXME: load from resource?")
                    .ConfigureAwait(false);
            }
            else
            {
                await _lazyModalManager
                    .Value
                    .ShowAndWaitMessageBoxAsync(
                        $"You lost the fight!\r\n" +
                        $"{encounterWonItems.Count} items were added to your inventory.\r\n" +
                        $"// FIXME: load from resource?")
                    .ConfigureAwait(false);
            }
        }
    }
}
