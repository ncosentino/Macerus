using System;
using System.Linq;
using System.Threading.Tasks;

using Macerus.Plugins.Features.Camera;
using Macerus.Plugins.Features.HeaderBar.Api.CombatTurnOrder;
using Macerus.Plugins.Features.Mapping;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Systems;
using ProjectXyz.Plugins.Features.Combat.Api;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Mapping;

namespace Macerus.Plugins.Features.HeaderBar.Default.CombatTurnOrder
{
    public sealed class CombatTurnOrderController : ICombatTurnOrderController
    {
        private readonly IFilterContextProvider _filterContextProvider;
        private readonly IObservableCombatTurnManager _combatTurnManager;
        private readonly ICombatTurnOrderViewModel _combatTurnOrderViewModel;
        private readonly IGameObjectToCombatTurnOrderPortraitConverter _gameObjectToCombatTurnOrderPortraitConverter;
        private readonly Lazy<ICameraManager> _lazyCameraManager;
        private readonly Lazy<IMappingAmenity> _lazyMappingAmenity;
        private readonly Lazy<IMapGameObjectManager> _lazyMapGameObjectManager;

        public CombatTurnOrderController(
            IFilterContextProvider filterContextProvider,
            IObservableCombatTurnManager combatTurnManager,
            ICombatTurnOrderViewModel combatTurnOrderViewModel,
            IGameObjectToCombatTurnOrderPortraitConverter gameObjectToCombatTurnOrderPortraitConverter,
            Lazy<ICameraManager> lazyCameraManager,
            Lazy<IMappingAmenity> lazyMappingAmenity,
            Lazy<IMapGameObjectManager> lazyMapGameObjectManager)
        {
            _filterContextProvider = filterContextProvider;
            _combatTurnManager = combatTurnManager;
            _combatTurnOrderViewModel = combatTurnOrderViewModel;
            _gameObjectToCombatTurnOrderPortraitConverter = gameObjectToCombatTurnOrderPortraitConverter;
            _lazyCameraManager = lazyCameraManager;
            _lazyMappingAmenity = lazyMappingAmenity;
            _lazyMapGameObjectManager = lazyMapGameObjectManager;

            _combatTurnManager.CombatStarted += CombatTurnManager_CombatStarted;
            _combatTurnManager.CombatEnded += CombatTurnManager_CombatEnded;
            // FIXME: does this defeat the point of lazy?
            _lazyMapGameObjectManager.Value.Synchronized += MapGameObjectManager_Synchronized;
        }

        public double UpdateIntervalInSeconds { get; } = TimeSpan.FromDays(99).TotalSeconds;

        public async Task UpdateAsync(ISystemUpdateContext systemUpdateContext)
        {
            // no-op
            return;
        }

        private void RefreshPortraits()
        {
            var filterContext = _filterContextProvider.GetContext();
            var turnOrderSnapshot = _combatTurnManager.GetSnapshot(
                filterContext,
                20);

            foreach (var portrait in _combatTurnOrderViewModel.Portraits)
            {
                portrait.Activated -= Portrait_Activated;
            }

            var portraits = turnOrderSnapshot
                .Select(_gameObjectToCombatTurnOrderPortraitConverter.Convert);
            _combatTurnOrderViewModel.UpdatePortraits(portraits);

            foreach (var portrait in _combatTurnOrderViewModel.Portraits)
            {
                portrait.Activated += Portrait_Activated;
            }
        }

        private void Portrait_Activated(
            object sender,
            EventArgs e)
        {
            var portrait = (ICombatTurnOrderPortraitViewModel)sender;
            var actor = _lazyMappingAmenity
                .Value
                .GameObjects
                .First(x => Equals(
                    x.GetOnly<IIdentifierBehavior>().Id,
                    portrait.ActorIdentifier));
            _lazyCameraManager.Value.SetFollowTarget(actor);
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

        private void MapGameObjectManager_Synchronized(
            object sender,
            GameObjectsSynchronizedEventArgs e) => RefreshPortraits();

        private void CombatTurnManager_CombatEnded(
            object sender,
            CombatEndedEventArgs e)
        {
            _combatTurnManager.TurnProgressed -= CombatTurnManager_TurnProgressed;
            _combatTurnOrderViewModel.Close();
        }
    }
}
