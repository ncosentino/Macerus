using System;

namespace Macerus.Game.Api
{
    public interface IReadOnlyUpdateFrequencyManager
    {
        int MaxUpdatesPerSecond { get; }

        TimeSpan UpdateStatusFrequency { get; }
    }
}
