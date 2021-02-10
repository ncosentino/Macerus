﻿using ProjectXyz.Api.Behaviors;

namespace Macerus.Api.Behaviors
{
    public interface IReadOnlyMovementBehavior : IBehavior
    {
        double ThrottleX { get; }

        double ThrottleY { get; }

        double VelocityX { get; }

        double VelocityY { get; }
    }
}