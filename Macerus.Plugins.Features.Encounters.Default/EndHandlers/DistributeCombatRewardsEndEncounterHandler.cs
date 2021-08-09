using System;
using System.Linq;
using System.Threading.Tasks;

using Macerus.Plugins.Features.Encounters.EndHandlers;
using Macerus.Plugins.Features.GameObjects.Actors;

using NexusLabs.Contracts;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.PartyManagement;

namespace Macerus.Plugins.Features.Encounters.Default.EndHandlers
{
    public sealed class DistributeCombatRewardsEndEncounterHandler : IDiscoverableEndEncounterHandler
    {
        private readonly Lazy<IRosterManager> _lazyRosterManager;
        private readonly IMacerusActorIdentifiers _macerusActorIdentifiers;

        public DistributeCombatRewardsEndEncounterHandler(
            Lazy<IRosterManager> lazyRosterManager,
            IMacerusActorIdentifiers macerusActorIdentifiers)
        {
            _lazyRosterManager = lazyRosterManager;
            _macerusActorIdentifiers = macerusActorIdentifiers;
        }

        public async Task<IGameObject> HandleAsync(
            IGameObject encounter,
            IFilterContext filterContext)
        {
            if (!encounter.TryGetFirst<IEncounterCombatRewardsBehavior>(out var combatRewardsBehavior))
            {
                return encounter;
            }

            var inventory = _lazyRosterManager
                .Value
                .ActivePartyLeader
                ?.Get<IItemContainerBehavior>()
                ?.SingleOrDefault(x => x.ContainerId.Equals(_macerusActorIdentifiers.InventoryIdentifier));
            Contract.RequiresNotNull(
                inventory,
                $"There was no inventory found to add the encounter rewards to.");
            foreach (var item in combatRewardsBehavior.Loot)
            {
                await Task.Yield();

                Contract.Requires(
                    inventory.TryAddItem(item),
                    $"Could not add encounter reward '{item}' to inventory '{inventory}'. " +
                    $"// FIXME: we need a forcing function to add items to inventories.");
            }

            if (combatRewardsBehavior.Experience > 0)
            {
                var experienceSlice = combatRewardsBehavior.Experience / (double)_lazyRosterManager.Value.ActiveParty.Count;
                foreach (var statsBehavior in _lazyRosterManager.Value.ActiveParty.Select(x => x.GetOnly<IHasMutableStatsBehavior>()))
                {
                    statsBehavior.MutateStats(stats => stats[_macerusActorIdentifiers.CurrentExperienceStatDefinitionId] += experienceSlice);
                }
            }

            return encounter;
        }
    }
}
