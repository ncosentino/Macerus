using System;

using Macerus.Game.Api;

namespace Macerus.Game
{
    public sealed class UpdateFrequencyManager : IUpdateFrequencyManager
    {
        private int _maxUpdatesPerSecond;
        private TimeSpan _updateStatusFrequency;

        public UpdateFrequencyManager()
        {
            MaxUpdatesPerSecond = 60;
            UpdateStatusFrequency = TimeSpan.FromSeconds(5);
        }

        public event EventHandler<EventArgs> MaxUpdatesPerSecondChanged;

        public event EventHandler<EventArgs> UpdateStatusFrequencyChanged;

        public int MaxUpdatesPerSecond
        {
            get => _maxUpdatesPerSecond;
            set
            {
                if (_maxUpdatesPerSecond == value)
                {
                    return;
                }

                _maxUpdatesPerSecond = value;
                MaxUpdatesPerSecondChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public TimeSpan UpdateStatusFrequency
        { 
            get => _updateStatusFrequency;
            set
            {
                if (_updateStatusFrequency == value)
                {
                    return;
                }

                _updateStatusFrequency = value;
                UpdateStatusFrequencyChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
