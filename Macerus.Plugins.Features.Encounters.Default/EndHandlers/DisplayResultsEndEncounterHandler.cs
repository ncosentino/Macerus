using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Macerus.Plugins.Features.Encounters.EndHandlers;
using Macerus.Plugins.Features.Gui;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.Filtering.Api;

namespace Macerus.Plugins.Features.Encounters.Default.EndHandlers
{
    public sealed class DisplayResultsEndEncounterHandler : IDiscoverableEndEncounterHandler
    {
        private readonly Lazy<IModalManager> _lazyModalManager;

        public DisplayResultsEndEncounterHandler(Lazy<IModalManager> lazyModalManager)
        {
            _lazyModalManager = lazyModalManager;
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

            var rewardsMessageBuilder = new StringBuilder();
            rewardsMessageBuilder.AppendLine(combatOutcomeBehavior.PlayerWon == true
                ? "You won the fight!"
                : "You lost the fight!");

            if (encounter.TryGetFirst<IEncounterCombatRewardsBehavior>(out var combatRewards))
            {
                if (combatRewards.Loot.Any())
                {
                    rewardsMessageBuilder.AppendLine($"{combatRewards.Loot.Count} items were added to your inventory.");
                }

                if (combatRewards.Experience > 0)
                {
                    rewardsMessageBuilder.AppendLine($"{combatRewards.Experience} experience was distributed to the active party.");
                }
            }

            rewardsMessageBuilder.AppendLine($"// FIXME: load all of this from resource?");

            await _lazyModalManager
                .Value
                .ShowAndWaitMessageBoxAsync(rewardsMessageBuilder.ToString())
                .ConfigureAwait(false);

            return encounter;
        }
    }
}
