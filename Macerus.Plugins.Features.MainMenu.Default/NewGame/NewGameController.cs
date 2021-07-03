using System;
using System.Threading.Tasks;

using Macerus.Game.Api.Scenes;
using Macerus.Plugins.Features.Gui.SceneTransitions;
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
        private readonly Lazy<INewGameWorkflow> _lazyNewGameWorkflow;
        private readonly Lazy<IMainMenuController> _lazyMainMenuController;

        public NewGameController(
            INewGameViewModel newGameViewModel,
            ISceneManager sceneManager,
            ILoadingScreenController loadingScreenController,
            ITransitionController transitionController,
            Lazy<INewGameWorkflow> lazyNewGameWorkflow,
            Lazy<IMainMenuController> lazyMainMenuController)
        {
            _newGameViewModel = newGameViewModel;
            _sceneManager = sceneManager;
            _loadingScreenController = loadingScreenController;
            _transitionController = transitionController;
            _lazyNewGameWorkflow = lazyNewGameWorkflow;
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
            Task createNewGameTask = null;
            _loadingScreenController.BeginLoad(
                async () =>
                {
                    CloseScreen();

                    // FIXME: what is this clusterf*ck with async call backs
                    // and starting up a new task all about? we must be able to
                    // simplify this nonsense
                    createNewGameTask = Task
                        .Run(async () => await _lazyNewGameWorkflow.Value.RunAsync())
                        .ContinueWith(t =>
                        {
                            _sceneManager.BeginNavigateToScene(
                                new StringIdentifier("Explore"),
                                sc => sceneCompletion = sc);
                        }, TaskContinuationOptions.OnlyOnRanToCompletion);
                },
                async () =>
                {
                    if (createNewGameTask.Status == TaskStatus.Faulted)
                    {
                        throw new InvalidOperationException(
                            $"Loading task failed. See inner exception.",
                            createNewGameTask.Exception);
                    }

                    var progress = (sceneCompletion != null && createNewGameTask.Status == TaskStatus.RanToCompletion)
                        ? 1
                        : 0;
                    return progress;
                },
                async () => sceneCompletion.SwitchoverScenes());
        }

        private void NewGameViewModel_RequestGoBack(
            object sender, 
            EventArgs e)
        {
            _transitionController.StartTransition(
                TimeSpan.FromSeconds(0.3),
                TimeSpan.FromSeconds(0.3),
                async () =>
                {
                    _lazyMainMenuController.Value.OpenMenu();
                    _newGameViewModel.Close();
                },
                async () => { });
        }
    }
}
