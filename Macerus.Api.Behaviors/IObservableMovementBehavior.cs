using System;

namespace Macerus.Api.Behaviors
{
    public interface IObservableMovementBehavior : IReadOnlyMovementBehavior
    {
        event EventHandler<EventArgs> ThrottleChanged;

        event EventHandler<EventArgs> VelocityChanged;
    }
}