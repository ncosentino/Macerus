using System;

using Macerus.Api.Behaviors;

using NexusLabs.Framework;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.Encounters.Triggers
{
    public sealed class EncounterTriggerHandler : IObservableEncounterTriggerHandler
    {
        private readonly IRandom _random;
        private readonly bool _mustBeMoving;
        private readonly double _chanceToTrigger;
        private readonly TimeSpan _triggerInterval;

        private DateTime? _lastMovementDetected;
        private TimeSpan _elapsed;

        public EncounterTriggerHandler(
            IRandom random,
            IObservableEncounterTrigger encounterTrigger,
            bool mustBeMoving,
            double chanceToTrigger, // FIXME: can this be affected by stats?
            TimeSpan triggerInterval) // FIXME: can this be affected by stats?
        {
            _elapsed = TimeSpan.FromSeconds(0);
            _random = random;
            _mustBeMoving = mustBeMoving;
            _chanceToTrigger = chanceToTrigger;
            _triggerInterval = triggerInterval;

            encounterTrigger.TriggerEnter += EncounterTrigger_TriggerEnter;
            encounterTrigger.TriggerExit += EncounterTrigger_TriggerExit;
            encounterTrigger.TriggerStay += EncounterTrigger_TriggerStay;
        }

        public event EventHandler<EventArgs> Encounter;

        private void EncounterTrigger_TriggerStay(
            object sender,
            GameObjectTriggerEventArgs e)
        {
            if (!e.CollidingGameObject.Has<IPlayerControlledBehavior>())
            {
                return;
            }

            var player = e.CollidingGameObject;
            if (_mustBeMoving)
            {
                var movementBehavior = player.GetOnly<IReadOnlyMovementBehavior>();
                if (movementBehavior.VelocityX == 0 && movementBehavior.VelocityY == 0)
                {
                    return;
                }
            }

            _elapsed += DateTime.UtcNow - _lastMovementDetected.Value;
            _lastMovementDetected = DateTime.UtcNow;

            if (_elapsed < _triggerInterval)
            {
                return;
            }

            _elapsed = TimeSpan.FromSeconds(0);

            if (_chanceToTrigger < _random.NextDouble(0, 1))
            {
                return;
            }

            Encounter?.Invoke(this, EventArgs.Empty);
        }

        private void EncounterTrigger_TriggerExit(
            object sender,
            GameObjectTriggerEventArgs e)
        {
            _lastMovementDetected = DateTime.UtcNow;
        }

        private void EncounterTrigger_TriggerEnter(
            object sender,
            GameObjectTriggerEventArgs e)
        {
            _lastMovementDetected = DateTime.UtcNow;
        }
    }
}
