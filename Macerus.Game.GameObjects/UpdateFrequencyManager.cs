using System;

using Macerus.Game.Api;

namespace Macerus.Game
{
    public sealed class UpdateFrequencyManager : IUpdateFrequencyManager
    {
        public int MaxUpdatesPerSecond { get; set; } = 200;

        public TimeSpan UpdateStatusFrequency { get; set; } = TimeSpan.FromSeconds(5);
    }
}
