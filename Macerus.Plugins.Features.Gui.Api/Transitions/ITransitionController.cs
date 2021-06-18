using System;
using System.Threading.Tasks;

namespace Macerus.Plugins.Features.Gui.Api.SceneTransitions
{
    public interface ITransitionController
    {
        bool Transitioning { get; }

        void StartTransition(
            TimeSpan transitionOutDuration,
            TimeSpan transitionInDuration,
            Func<Task> transitionedOutCallbackAsync,
            Func<Task> transitionedInCallbackAsync);
    }
}
