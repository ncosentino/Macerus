using System;

namespace Macerus.Plugins.Features.Encounters.Triggers
{
    public interface IObservableEncounterTriggerHandler
    {
        event EventHandler<EventArgs> Encounter;
    }
}