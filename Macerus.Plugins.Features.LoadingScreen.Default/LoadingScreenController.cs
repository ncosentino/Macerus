using System;

using Macerus.Game.Api.Scenes;
using Macerus.Plugins.Features.Gui.Api.SceneTransitions;
using Macerus.Plugins.Features.LoadingScreen.Api;

using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.LoadingScreen.Default
{
    public sealed class LoadingScreenController : ILoadingScreenController
    {
        private readonly ILoadingScreenViewModel _loadingScreenViewModel;
        private readonly ISceneManager _sceneManager;
        private readonly ISceneTransitionController _sceneTransitionController;

        public LoadingScreenController(
            ILoadingScreenViewModel loadingScreenViewModel,
            ISceneManager sceneManager,
            ISceneTransitionController sceneTransitionController)
        {
            _loadingScreenViewModel = loadingScreenViewModel;
            _sceneManager = sceneManager;
            _sceneTransitionController = sceneTransitionController;
        }

        public void Load(
            Action doWorkCallback,
            Action doWhenDoneCallback)
        {
            const double TRANSITION_DURATION_IN_SEC = 0.5;
            const double TRANSITION_DURATION_OUT_SEC = 0.5;
            _sceneTransitionController.StartTransition(
                TimeSpan.FromSeconds(TRANSITION_DURATION_OUT_SEC),
                TimeSpan.FromSeconds(TRANSITION_DURATION_IN_SEC),
                () => _sceneManager.GoToScene(new StringIdentifier("LoadingScreen")),
                () =>
                {
                    doWorkCallback?.Invoke();

                    _sceneTransitionController.StartTransition(
                        TimeSpan.FromSeconds(TRANSITION_DURATION_OUT_SEC),
                        TimeSpan.FromSeconds(TRANSITION_DURATION_IN_SEC),
                        () => doWhenDoneCallback?.Invoke(),
                        () => { });
                });
        }
    }
}
