using System;

using Macerus.Game.Api;
using Macerus.Game.Api.Scenes;
using Macerus.Plugins.Features.Gui.Api.SceneTransitions;
using Macerus.Plugins.Features.MainMenu.Api;

using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.MainMenu.Default
{
    public sealed class MainMenuController : IMainMenuController
    {
        private readonly IMainMenuViewModel _mainMenuViewModel;
        private readonly ISceneManager _sceneManager;
        private readonly IApplication _application;
        private readonly ISceneTransitionController _sceneTransitionController;

        public MainMenuController(
            IMainMenuViewModel mainMenuViewModel,
            ISceneManager sceneManager,
            IApplication application,
            ISceneTransitionController sceneTransitionController)
        {
            _mainMenuViewModel = mainMenuViewModel;
            _sceneManager = sceneManager;
            _application = application;
            _sceneTransitionController = sceneTransitionController;
            _mainMenuViewModel.RequestExit += MainMenuViewModel_RequestExit;
            _mainMenuViewModel.RequestNewGame += MainMenuViewModel_RequestNewGame;
            _mainMenuViewModel.RequestOptions += MainMenuViewModel_RequestOptions;
        }

        public void OpenMenu() => _mainMenuViewModel.Open();

        public void CloseMenu() => _mainMenuViewModel.Close();

        public bool ToggleMenu()
        {
            if (_mainMenuViewModel.IsOpen)
            {
                CloseMenu();
            }
            else
            {
                OpenMenu();
            }

            return _mainMenuViewModel.IsOpen;
        }

        private void MainMenuViewModel_RequestOptions(
            object sender,
            EventArgs e)
        {
            // FIXME: navigate to options pls
        }

        private void MainMenuViewModel_RequestNewGame(
            object sender,
            EventArgs e)
        {
            _sceneTransitionController.StartTransition(
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(1),
                () => _sceneManager.GoToScene(new StringIdentifier("Explore")),
                () => { });
        }

        private void MainMenuViewModel_RequestExit(
            object sender,
            EventArgs e)
        {
            _application.Exit();
        }
    }
}
