using System;

namespace Macerus.Game.Api
{
    public interface IUpdateFrequencyManager : IObservableUpdateFrequencyManager
    {
        new int MaxUpdatesPerSecond { get; set; }

        new TimeSpan UpdateStatusFrequency { get; set; }
    }
}
