﻿using ProjectXyz.Plugins.Features.TimeOfDay.Default;

namespace Macerus.Plugins.Content.TimeOfDay
{
    public sealed class TimeOfDayConfiguration : ITimeOfDayConfiguration
    {
        public double LengthOfDayInTurns { get; } = 10;
    }
}
