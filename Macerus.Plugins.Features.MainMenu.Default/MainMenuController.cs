using System;

using Macerus.Game.Api;
using Macerus.Plugins.Features.Gui.SceneTransitions;
using Macerus.Plugins.Features.MainMenu.Api;
using Macerus.Plugins.Features.MainMenu.Api.NewGame;

namespace Macerus.Plugins.Features.MainMenu.Default
{
    public sealed class MainMenuController : IMainMenuController
    {
        private readonly IMainMenuViewModel _mainMenuViewModel;
        private readonly IApplication _application;
        private readonly Lazy<INewGameController> _lazyNewGameController;
        private readonly ITransitionController _transitionController;

        public MainMenuController(
            IMainMenuViewModel mainMenuViewModel,
            IApplication application,
            Lazy<INewGameController> lazyNewGameController,
            ITransitionController transitionController)
        {
            _mainMenuViewModel = mainMenuViewModel;
            _application = application;
            _lazyNewGameController = lazyNewGameController;
            _transitionController = transitionController;
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
            _transitionController.StartTransition(
                TimeSpan.FromSeconds(0.3),
                TimeSpan.FromSeconds(0.3),
                async () =>
                {
                    _lazyNewGameController.Value.ShowScreen();
                    _mainMenuViewModel.Close();
                },
                async () => { });
        }

        private void MainMenuViewModel_RequestExit(
            object sender,
            EventArgs e)
        {
            _application.Exit();
        }
    }
}
