using System;

namespace Macerus.Game.Api
{
    public interface IObservableUpdateFrequencyManager : IReadOnlyUpdateFrequencyManager
    {
        event EventHandler<EventArgs> MaxUpdatesPerSecondChanged;

        event EventHandler<EventArgs> UpdateStatusFrequencyChanged;
    }
}
