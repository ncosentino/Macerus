using System;
using System.Collections.Generic;

using Macerus.Game.Api;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Logging;
using ProjectXyz.Api.Systems;

namespace Macerus.Game
{
    public sealed class UpdateFrequencySystem : IDiscoverableSystem
    {
        private readonly IUpdateFrequencyManager _updateFrequencyManager;
        private readonly ILogger _logger;

        private int _updatesAccumulator;
        private DateTime _nextTrigger;
        private DateTime _lastTrigger;
        private DateTime _lastUpdate;

        public UpdateFrequencySystem(
            IUpdateFrequencyManager updateFrequencyManager,
            ILogger logger)
        {
            _updateFrequencyManager = updateFrequencyManager;
            _logger = logger;
        }

        public int? Priority { get; } = int.MinValue;

        public void Update(
            ISystemUpdateContext systemUpdateContext,
            IEnumerable<IGameObject> gameObjects)
        {
            _updatesAccumulator += 1;

            var elapsedMs = (DateTime.UtcNow - _lastUpdate).Milliseconds;
            var limitInMs = 1000 / _updateFrequencyManager.MaxUpdatesPerSecond;
            if (elapsedMs < limitInMs)
            {
                System.Threading.Thread.Sleep(limitInMs - elapsedMs);
            }

            if (DateTime.UtcNow >= _nextTrigger)
            {
                _logger.Debug(
                    $"System Update Rate: {_updatesAccumulator / (DateTime.UtcNow - _lastTrigger).TotalSeconds:00}/sec");

                _updatesAccumulator = 0;
                _lastTrigger = _nextTrigger;
                _nextTrigger = DateTime.UtcNow + _updateFrequencyManager.UpdateStatusFrequency;
            }

            _lastUpdate = DateTime.UtcNow;
        }
    }
}
