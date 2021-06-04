using System;

namespace Macerus.Game.Api
{
    public interface IUpdateFrequencyManager
    {
        int MaxUpdatesPerSecond { get; set; }

        TimeSpan UpdateStatusFrequency { get; set; }
    }
}
