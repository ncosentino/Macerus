using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Encounters.Triggers
{
    public interface IReadOnlyEncounterTriggerPropertiesBehavior : IBehavior
    {
        double EncounterChance { get; }

        IIdentifier EncounterId { get; }

        IInterval EncounterInterval { get; }

        bool MustBeMoving { get; }
    }
}