using System;

namespace Macerus.Plugins.Features.Encounters.Triggers
{
    public interface IObservableEncounterTrigger
    {
        event EventHandler<GameObjectTriggerEventArgs> TriggerEnter;

        event EventHandler<GameObjectTriggerEventArgs> TriggerExit;

        event EventHandler<GameObjectTriggerEventArgs> TriggerStay;
    }
}
