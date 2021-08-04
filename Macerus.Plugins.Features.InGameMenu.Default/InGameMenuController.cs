using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Game.Api;
using Macerus.Game.Api.Scenes;
using Macerus.Plugins.Features.DataPersistence;
using Macerus.Plugins.Features.Gui;
using Macerus.Plugins.Features.Gui.SceneTransitions;
using Macerus.Plugins.Features.InGameMenu.Api;
using Macerus.Plugins.Features.MainMenu.Api;
using Macerus.Plugins.Features.StatusBar.Api;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Combat.Api;
using ProjectXyz.Plugins.Features.Mapping;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.InGameMenu.Default
{
    public sealed class InGameMenuController : IInGameMenuController
    {
        private readonly IInGameMenuViewModel _inGameMenuViewModel;
        private readonly ISceneManager _sceneManager;
        private readonly IApplication _application;
        private readonly ITransitionController _transitionController;
        private readonly IMainMenuController _mainMenuController;
        private readonly Lazy<IModalManager> _lazyModalManager;
        private readonly Lazy<IMapManager> _lazyMapManager;
        private readonly Lazy<ICombatTurnManager> _lazyCombatTurnManager;
        private readonly Lazy<IDataPersistenceManager> _lazyDayaPersistenceManager;
        private readonly Lazy<IStatusBarViewModel> _lazyStatusBarViewModel;

        public InGameMenuController(
            IInGameMenuViewModel inGameMenuViewModel,
            ISceneManager sceneManager,
            IApplication application,
            ITransitionController transitionController,
            IMainMenuController mainMenuController,
            Lazy<IModalManager> lazyModalManager,
            Lazy<IMapManager> lazyMapManager,
            Lazy<ICombatTurnManager> lazyCombatTurnManager,
            Lazy<IDataPersistenceManager> lazyDayaPersistenceManager,
            Lazy<IStatusBarViewModel> lazyStatusBarViewModel)
        {
            _inGameMenuViewModel = inGameMenuViewModel;
            _sceneManager = sceneManager;
            _application = application;
            _transitionController = transitionController;
            _mainMenuController = mainMenuController;
            _lazyModalManager = lazyModalManager;
            _lazyMapManager = lazyMapManager;
            _lazyCombatTurnManager = lazyCombatTurnManager;
            _lazyDayaPersistenceManager = lazyDayaPersistenceManager;
            _lazyStatusBarViewModel = lazyStatusBarViewModel;
            _inGameMenuViewModel.RequestExit += InGameMenuViewModel_RequestExit;
            _inGameMenuViewModel.RequestClose += InGameMenuViewModel_RequestClose;
            _inGameMenuViewModel.RequestOptions += InGameMenuViewModel_RequestOptions;
            _inGameMenuViewModel.RequestGoToMainMenu += InGameMenuViewModel_RequestGoToMainMenu;
            _inGameMenuViewModel.RequestSaveGame += InGameMenuViewModel_RequestSaveGame;
            _inGameMenuViewModel.RequestLoadGame += InGameMenuViewModel_RequestLoadGame;
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

        private async void InGameMenuViewModel_RequestGoToMainMenu(
            object sender,
            EventArgs e)
        {
            if (!await _lazyModalManager
                .Value
                .ShowAndWaitYesNoAsync(
                    "Would you like to go to the main menu? All " +
                    "unsaved progress will be lost.\r\n" +
                    "// FIXME: load this from a resource ID")
                .ConfigureAwait(false))
            {
                return;
            }

            _transitionController.StartTransition(
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(1),
                async () =>
                {
                    _lazyStatusBarViewModel.Value.IsOpen = false;

                    // FIXME: we want to probably have common logic across all
                    // the spots that do this game state resetting
                    _lazyCombatTurnManager.Value.EndCombat(
                        Enumerable.Empty<IGameObject>(),
                        new Dictionary<int, IReadOnlyCollection<IGameObject>>());
                    await _lazyMapManager
                        .Value
                        .UnloadMapAsync()
                        .ConfigureAwait(false);
                    _mainMenuController.OpenMenu();
                    _sceneManager.NavigateToScene(new StringIdentifier("MainMenu"));
                },
                async () => { });
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

        // FIXME: actually open up a save/load menu for this
        private static readonly StringIdentifier FAKE_GAME_ID = new StringIdentifier("my-save-gane");
        private void InGameMenuViewModel_RequestSaveGame(object sender, EventArgs e)
        {
            _transitionController.StartTransition(
                TimeSpan.FromSeconds(0.3),
                TimeSpan.FromSeconds(0.3),
                async () =>
                {
                    await _lazyDayaPersistenceManager
                        .Value
                        .SaveAsync(FAKE_GAME_ID)
                        .ConfigureAwait(false);
                    CloseMenu();
                },
                async () => { });
        }

        // FIXME: actually open up a save/load menu for this
        private async void InGameMenuViewModel_RequestLoadGame(object sender, EventArgs e)
        {
            if (!await _lazyModalManager
                .Value
                .ShowAndWaitYesNoAsync(
                    "Are you sure you'd like to load from this save? All " +
                    "unsaved progress will be lost.\r\n" +
                    "// FIXME: load this from a resource ID")
                .ConfigureAwait(false))
            {
                return;
            }

            _transitionController.StartTransition(
                TimeSpan.FromSeconds(0.3),
                TimeSpan.FromSeconds(0.3),
                async () =>
                {
                    // FIXME: we want to probably have common logic across all
                    // the spots that do this game state resetting
                    _lazyCombatTurnManager.Value.EndCombat(
                        Enumerable.Empty<IGameObject>(),
                        new Dictionary<int, IReadOnlyCollection<IGameObject>>());
                    await _lazyMapManager
                        .Value
                        .UnloadMapAsync()
                        .ConfigureAwait(false);
                    await _lazyDayaPersistenceManager
                        .Value
                        .LoadAsync(FAKE_GAME_ID)
                        .ConfigureAwait(false);
                    CloseMenu();
                },
                async () => { });
        }
    }
}
