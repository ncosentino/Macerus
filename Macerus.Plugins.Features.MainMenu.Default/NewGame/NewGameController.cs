using System;

using Macerus.Game.Api.Scenes;
using Macerus.Plugins.Features.Gui.Api.SceneTransitions;
using Macerus.Plugins.Features.LoadingScreen.Api;
using Macerus.Plugins.Features.MainMenu.Api;
using Macerus.Plugins.Features.MainMenu.Api.NewGame;

using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.MainMenu.Default.NewGame
{
    public sealed class NewGameController : INewGameController
    {
        private readonly INewGameViewModel _newGameViewModel;
        private readonly ISceneManager _sceneManager;
        private readonly ILoadingScreenController _loadingScreenController;
        private readonly ITransitionController _transitionController;
        private readonly Lazy<IMainMenuController> _lazyMainMenuController;

        public NewGameController(
            INewGameViewModel newGameViewModel,
            ISceneManager sceneManager,
            ILoadingScreenController loadingScreenController,
            ITransitionController transitionController,
            Lazy<IMainMenuController> lazyMainMenuController)
        {
            _newGameViewModel = newGameViewModel;
            _sceneManager = sceneManager;
            _loadingScreenController = loadingScreenController;
            _transitionController = transitionController;
            _lazyMainMenuController = lazyMainMenuController;
            _newGameViewModel.RequestNewGame += NewGameViewModel_RequestNewGame;
            _newGameViewModel.RequestGoBack += NewGameViewModel_RequestGoBack;
        }

        public void ShowScreen() => _newGameViewModel.Open();

        public void CloseScreen() => _newGameViewModel.Close();

        public bool ToggleScreen()
        {
            if (_newGameViewModel.IsOpen)
            {
                CloseScreen();
            }
            else
            {
                ShowScreen();
            }

            return _newGameViewModel.IsOpen;
        }

        private void NewGameViewModel_RequestNewGame(
            object sender,
            EventArgs e)
        {
            ISceneCompletion sceneCompletion = null;
            _loadingScreenController.BeginLoad(
                () =>
                {
                    CloseScreen();
                    _sceneManager.BeginNavigateToScene(
                        new StringIdentifier("Explore"),
                        sc => sceneCompletion = sc);
                },
                () => sceneCompletion != null ? 1 : 0,
                () => sceneCompletion.SwitchoverScenes());
        }

        private void NewGameViewModel_RequestGoBack(
            object sender, 
            EventArgs e)
        {
            _transitionController.StartTransition(
                TimeSpan.FromSeconds(0.3),
                TimeSpan.FromSeconds(0.3),
                () =>
                {
                    _lazyMainMenuController.Value.OpenMenu();
                    _newGameViewModel.Close();
                },
                () => { });
        }
    }
}
