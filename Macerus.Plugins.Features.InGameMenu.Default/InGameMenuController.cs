using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Game.Api;
using Macerus.Game.Api.Scenes;
using Macerus.Plugins.Features.Gui.Api.SceneTransitions;
using Macerus.Plugins.Features.InGameMenu.Api;
using Macerus.Plugins.Features.MainMenu.Api;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Combat.Api;
using ProjectXyz.Plugins.Features.Mapping.Api;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.InGameMenu.Default
{
    public sealed class InGameMenuController : IInGameMenuController
    {
        private readonly IInGameMenuViewModel _inGameMenuViewModel;
        private readonly ISceneManager _sceneManager;
        private readonly IApplication _application;
        private readonly ITransitionController _sceneTransitionController;
        private readonly IMainMenuController _mainMenuController;
        private readonly Lazy<IMapManager> _lazyMapManager;
        private readonly Lazy<ICombatTurnManager> _lazyCombatTurnManager;

        public InGameMenuController(
            IInGameMenuViewModel inGameMenuViewModel,
            ISceneManager sceneManager,
            IApplication application,
            ITransitionController sceneTransitionController,
            IMainMenuController mainMenuController,
            Lazy<IMapManager> lazyMapManager,
            Lazy<ICombatTurnManager> lazyCombatTurnManager)
        {
            _inGameMenuViewModel = inGameMenuViewModel;
            _sceneManager = sceneManager;
            _application = application;
            _sceneTransitionController = sceneTransitionController;
            _mainMenuController = mainMenuController;
            _lazyMapManager = lazyMapManager;
            _lazyCombatTurnManager = lazyCombatTurnManager;
            _inGameMenuViewModel.RequestExit += InGameMenuViewModel_RequestExit;
            _inGameMenuViewModel.RequestClose += InGameMenuViewModel_RequestClose;
            _inGameMenuViewModel.RequestOptions += InGameMenuViewModel_RequestOptions;
            _inGameMenuViewModel.RequestGoToMainMenu += InGameMenuViewModel_RequestGoToMainMenu;
        }

        public void OpenMenu() => _inGameMenuViewModel.Open();

        public void CloseMenu() => _inGameMenuViewModel.Close();

        public bool ToggleMenu()
        {
            if (_inGameMenuViewModel.IsOpen)
            {
                CloseMenu();
            }
            else
            {
                OpenMenu();
            }

            return _inGameMenuViewModel.IsOpen;
        }

        private void InGameMenuViewModel_RequestOptions(
            object sender,
            EventArgs e)
        {
            // FIXME: navigate to options pls
        }

        private void InGameMenuViewModel_RequestGoToMainMenu(
            object sender,
            EventArgs e)
        {
            _sceneTransitionController.StartTransition(
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(1),
                () =>
                {
                    _lazyCombatTurnManager.Value.EndCombat(
                        Enumerable.Empty<IGameObject>(),
                        new Dictionary<int, IReadOnlyCollection<IGameObject>>());
                    _lazyMapManager.Value.UnloadMap();
                    _mainMenuController.OpenMenu();
                    _sceneManager.NavigateToScene(new StringIdentifier("MainMenu"));
                },
                () => { });
        }

        private void InGameMenuViewModel_RequestExit(
            object sender,
            EventArgs e)
        {
            _application.Exit();
        }

        private void InGameMenuViewModel_RequestClose(
            object sender,
            EventArgs e)
        {
            CloseMenu();
        }
    }
}
