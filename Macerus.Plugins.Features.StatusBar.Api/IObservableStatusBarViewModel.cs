using System;

namespace Macerus.Plugins.Features.StatusBar.Api
{
    public interface IObservableStatusBarViewModel : IReadOnlyStatusBarViewModel
    {
        event EventHandler<EventArgs> RequestCompleteTurn;
    }
}
