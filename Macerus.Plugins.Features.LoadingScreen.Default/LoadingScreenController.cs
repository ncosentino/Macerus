using System;
using System.Threading.Tasks;

using Macerus.Game.Api.Scenes;
using Macerus.Plugins.Features.Gui.Api;
using Macerus.Plugins.Features.Gui.Api.SceneTransitions;
using Macerus.Plugins.Features.LoadingScreen.Api;

using NexusLabs.Contracts;

using ProjectXyz.Api.Systems;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.LoadingScreen.Default
{
    public sealed class LoadingScreenController :
        ILoadingScreenController,
        IDiscoverableUserInterfaceUpdate
    {
        private enum LoadState
        {
            None,
            Start,
            Starting,
            StartWork,
            WaitForWork,
            StartCompletion,
            Completing,
        }

        private const double INTERMEDIATE_TRANSITION_DURATION_IN_SEC = 0.5;
        private const double INTERMEDIATE_TRANSITION_DURATION_OUT_SEC = 0.5;
        private const double FINAL_TRANSITION_DURATION_IN_SEC = 2.0;

        private readonly ILoadingScreenViewModel _loadingScreenViewModel;
        private readonly ISceneManager _sceneManager;
        private readonly ITransitionController _sceneTransitionController;

        private LoadState _loadState;
        private Func<Task> _startWorkCallbackAsync;
        private Func<Task<double>> _checkWorkProgressCallbackAsync;
        private Func<Task> _doWhenDoneCallbackAsync;

        public LoadingScreenController(
            ILoadingScreenViewModel loadingScreenViewModel,
            ISceneManager sceneManager,
            ITransitionController sceneTransitionController)
        {
            _loadingScreenViewModel = loadingScreenViewModel;
            _sceneManager = sceneManager;
            _sceneTransitionController = sceneTransitionController;
        }

        public bool IsLoading => _loadState != LoadState.None;

        public double UpdateIntervalInSeconds => IsLoading
            ? 0.1
            : 10;

        public void BeginLoad(
            Func<Task> startWorkCallbackAsync,
            Func<Task<double>> checkWorkProgressCallbackAsync,
            Func<Task> doWhenDoneCallbackAsync)
        {
            Contract.Requires(
                !IsLoading,
                "The controller is already performing a loading operation.");

            _startWorkCallbackAsync = startWorkCallbackAsync;
            _checkWorkProgressCallbackAsync = checkWorkProgressCallbackAsync;
            _doWhenDoneCallbackAsync = doWhenDoneCallbackAsync;
            _loadState = LoadState.Start;
        }

        public async Task UpdateAsync(ISystemUpdateContext systemUpdateContext)
        {
            if (_loadState == LoadState.None ||
                _loadState == LoadState.Starting ||
                _loadState == LoadState.Completing)
            {
                return;
            }

            if (_loadState == LoadState.Start)
            {
                _sceneTransitionController.StartTransition(
                    TimeSpan.FromSeconds(INTERMEDIATE_TRANSITION_DURATION_OUT_SEC),
                    TimeSpan.FromSeconds(INTERMEDIATE_TRANSITION_DURATION_IN_SEC),
                    async () => _sceneManager.BeginNavigateToScene(
                        new StringIdentifier("LoadingScreen"),
                        sc =>
                        {
                            _loadState = LoadState.StartWork;
                            sc.SwitchoverScenes();
                        }),
                    async () => { });
                _loadState = LoadState.Starting;
            }

            if (_loadState == LoadState.StartWork)
            {
                await _startWorkCallbackAsync.Invoke();
                _loadState = LoadState.WaitForWork;
            }

            if (_loadState == LoadState.WaitForWork)
            {
                var progress = await _checkWorkProgressCallbackAsync.Invoke();
                progress = Math.Min(1, Math.Max(0, progress));

                // FIXME: update view model

                if (progress >= 1)
                {
                    _loadState = LoadState.StartCompletion;
                }
            }

            if (_loadState == LoadState.StartCompletion)
            {
                _sceneTransitionController.StartTransition(
                    TimeSpan.FromSeconds(INTERMEDIATE_TRANSITION_DURATION_OUT_SEC),
                    TimeSpan.FromSeconds(FINAL_TRANSITION_DURATION_IN_SEC),
                    async () => await (_doWhenDoneCallbackAsync?.Invoke() ?? Task.CompletedTask),
                    async () => _loadState = LoadState.None);
                _loadState = LoadState.Completing;
            }
        }
    }
}
