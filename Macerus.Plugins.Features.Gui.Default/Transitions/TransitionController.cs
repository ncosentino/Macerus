using System;
using System.Threading.Tasks;

using Macerus.Plugins.Features.Gui.Api;
using Macerus.Plugins.Features.Gui.Api.SceneTransitions;

using ProjectXyz.Api.Systems;

namespace Macerus.Plugins.Features.Gui.Default.SceneTransitions
{
    public sealed class TransitionController :
        ITransitionController,
        IDiscoverableUserInterfaceUpdate
    {
        private readonly IFaderSceneTransitionViewModel _faderSceneTransitionViewModel;

        private TransitionPackage _transitionPackage;
        private DateTime _lastUpdate;

        public TransitionController(IFaderSceneTransitionViewModel faderSceneTransitionViewModel)
        {
            _faderSceneTransitionViewModel = faderSceneTransitionViewModel;
        }

        public bool Transitioning => _transitionPackage != null;

        public double UpdateIntervalInSeconds => Transitioning
            ? 0.05
            : 10;

        public async Task UpdateAsync(ISystemUpdateContext systemUpdateContext)
        {
            if (!Transitioning)
            {
                return;
            }

            var transitionDurationSeconds = _transitionPackage.TransitionOut
                ? _transitionPackage.TransitionOutDuration.TotalSeconds
                : _transitionPackage.TransitionInDuration.TotalSeconds;
            var progress = transitionDurationSeconds <= 0
                ? 1
                : _transitionPackage.ElapsedPhaseTime.TotalSeconds / transitionDurationSeconds;
            progress = Math.Min(1, Math.Max(0, progress));

            var opacity = _transitionPackage.TransitionOut
                ? progress
                : 1 - progress;
            _faderSceneTransitionViewModel.Opacity = opacity;

            if (progress >= 1)
            {
                if (_transitionPackage.TransitionOut)
                {
                    _transitionPackage.TransitionedOutCallback?.Invoke();
                    _transitionPackage.TransitionOut = false;
                    _transitionPackage.ElapsedPhaseTime = TimeSpan.FromSeconds(0);
                }
                else
                {
                    _transitionPackage.TransitionedInCallback?.Invoke();
                    _transitionPackage = null;
                }

                return;
            }

            var now = DateTime.UtcNow;
            var elapsedSeconds = _lastUpdate == DateTime.MinValue
                ? 0
                : (now - _lastUpdate).TotalSeconds;
            _transitionPackage.ElapsedPhaseTime += TimeSpan.FromSeconds(elapsedSeconds);
            _lastUpdate = now;
        }

        public void StartTransition(
            TimeSpan transitionOutDuration,
            TimeSpan transitionInDuration,
            Action transitionedOutCallback,
            Action transitionedInCallback)
        {
            _lastUpdate = DateTime.UtcNow;
            _transitionPackage = new TransitionPackage()
            {
                TransitionOutDuration = transitionOutDuration,
                TransitionInDuration = transitionInDuration,
                ElapsedPhaseTime = TimeSpan.FromSeconds(0),
                TransitionedInCallback = transitionedInCallback,
                TransitionedOutCallback = transitionedOutCallback,
                TransitionOut = true,
            };
        }

        private sealed class TransitionPackage
        {
            public bool TransitionOut { get; set; }
            
            public TimeSpan TransitionOutDuration { get; set; }

            public TimeSpan TransitionInDuration { get; set; }

            public TimeSpan ElapsedPhaseTime { get; set; }

            public Action TransitionedOutCallback { get; set; }
            
            public Action TransitionedInCallback { get; set; }
        }
    }
}
