﻿using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.TimeOfDay.Default;

namespace Macerus.Content.TimeOfDay
{
    public sealed class TimeOfDayConverter : ITimeOfDayConverter
    {
        public IIdentifier GetTimeOfDay(double timeCyclePercent)
        {
            if (timeCyclePercent >= 0 && timeCyclePercent < 0.25)
            {
                return TimesOfDay.Night;
            }
            else if (timeCyclePercent >= 0.25 && timeCyclePercent < 0.5)
            {
                return TimesOfDay.Dawn;
            }
            else if (timeCyclePercent >= 0.25 && timeCyclePercent < 0.5)
            {
                return TimesOfDay.Day;
            }
            else
            {
                return TimesOfDay.Dusk;
            }
        }
    }
}
