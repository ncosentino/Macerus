using System;

namespace Macerus.Api.Behaviors
{
    public interface IObservableWorldLocationBehavior : IReadOnlyWorldLocationBehavior
    {
        event EventHandler<EventArgs> WorldLocationChanged;

        event EventHandler<EventArgs> SizeChanged;
    }
}