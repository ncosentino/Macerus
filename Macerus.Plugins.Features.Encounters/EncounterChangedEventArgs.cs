using System;

using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.Encounters
{
    public sealed class EncounterChangedEventArgs : EventArgs
    {
        public EncounterChangedEventArgs(
            IGameObject newValue,
            IGameObject previousValue)
        {
            NewValue = newValue;
            PreviousValue = previousValue;
        }

        public IGameObject NewValue { get; }

        public IGameObject PreviousValue { get; }
    }
}