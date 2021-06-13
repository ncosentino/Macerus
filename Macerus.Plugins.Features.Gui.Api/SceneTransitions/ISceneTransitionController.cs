using System;

namespace Macerus.Plugins.Features.Gui.Api.SceneTransitions
{
    public interface ISceneTransitionController
    {
        bool Transitioning { get; }

        void StartTransition(
            TimeSpan transitionOutDuration,
            TimeSpan transitionInDuration,
            Action transitionedOutCallback,
            Action transitionedInCallback);
    }
}
