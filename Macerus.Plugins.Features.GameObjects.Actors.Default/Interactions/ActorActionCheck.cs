using System.Linq;

using Macerus.Plugins.Features.GameObjects.Actors.Interactions;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Combat.Api;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;

namespace Macerus.Plugins.Features.GameObjects.Actors.Default.Interactions
{
    public sealed class ActorActionCheck : IActorActionCheck
    {
        private readonly IReadOnlyCombatTurnManager _combatTurnManager;
        private readonly IFilterContextProvider _filterContextProvider;

        public ActorActionCheck(
            IReadOnlyCombatTurnManager combatTurnManager,
            IFilterContextProvider filterContextProvider)
        {
            _combatTurnManager = combatTurnManager;
            _filterContextProvider = filterContextProvider;
        }

        public bool CanAct(IGameObject actor)
        {
            var isActivePlayerControlled =
                actor.TryGetFirst<IReadOnlyPlayerControlledBehavior>(out var playerControlledBehavior) &&
                playerControlledBehavior.IsActive;
            var isCurrentCombatTurn =
                _combatTurnManager.InCombat &&
                _combatTurnManager
                    .GetSnapshot(_filterContextProvider.GetContext(), 1)
                    .Single() == actor;
            bool canAct =
                isActivePlayerControlled &&
                (!_combatTurnManager.InCombat || isCurrentCombatTurn);
            return canAct;
        }
    }
}
