using ProjectXyz.Plugins.Features.TimeOfDay.Default;

namespace Macerus.Content.TimeOfDay
{
    public sealed class TimeOfDayConfiguration : ITimeOfDayConfiguration
    {
        public double LengthOfDayInTurns { get; } = 10;
    }
}
