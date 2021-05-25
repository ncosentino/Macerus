using System.Linq;

using Macerus.Plugins.Features.HeaderBar.Api.CombatTurnOrder;

using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Combat.Api;

namespace Macerus.Plugins.Features.HeaderBar.Default.CombatTurnOrder
{
    public sealed class CombatTurnOrderController : ICombatTurnOrderController
    {
        private readonly IFilterContextProvider _filterContextProvider;
        private readonly IObservableCombatTurnManager _combatTurnManager;
        private readonly ICombatTurnOrderViewModel _combatTurnOrderViewModel;
        private readonly IGameObjectToCombatTurnOrderPortraitConverter _gameObjectToCombatTurnOrderPortraitConverter;

        public CombatTurnOrderController(
            IFilterContextProvider filterContextProvider,
            IObservableCombatTurnManager combatTurnManager,
            ICombatTurnOrderViewModel combatTurnOrderViewModel,
            IGameObjectToCombatTurnOrderPortraitConverter gameObjectToCombatTurnOrderPortraitConverter)
        {
            _filterContextProvider = filterContextProvider;
            _combatTurnManager = combatTurnManager;
            _combatTurnOrderViewModel = combatTurnOrderViewModel;
            _gameObjectToCombatTurnOrderPortraitConverter = gameObjectToCombatTurnOrderPortraitConverter;

            _combatTurnManager.CombatStarted += CombatTurnManager_CombatStarted;
            _combatTurnManager.CombatEnded += CombatTurnManager_CombatEnded;
        }

        private void RefreshPortraits()
        {
            var filterContext = _filterContextProvider.GetContext();
            var turnOrderSnapshot = _combatTurnManager.GetSnapshot(
                filterContext,
                20);
            var portraits = turnOrderSnapshot
                .Select(_gameObjectToCombatTurnOrderPortraitConverter.Convert);
            _combatTurnOrderViewModel.UpdatePortraits(portraits);
        }

        private void CombatTurnManager_CombatStarted(
            object sender,
            CombatStartedEventArgs e)
        {
            RefreshPortraits();
            _combatTurnOrderViewModel.Open();
            _combatTurnManager.TurnProgressed += CombatTurnManager_TurnProgressed;
        }

        private void CombatTurnManager_TurnProgressed(
            object sender,
            TurnProgressedEventArgs e) => RefreshPortraits();

        private void CombatTurnManager_CombatEnded(
            object sender,
            CombatEndedEventArgs e)
        {
            _combatTurnManager.TurnProgressed -= CombatTurnManager_TurnProgressed;
            _combatTurnOrderViewModel.Close();
        }
    }
}
