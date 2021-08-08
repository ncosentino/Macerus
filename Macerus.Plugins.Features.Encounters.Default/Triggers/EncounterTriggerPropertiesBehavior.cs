using Macerus.Plugins.Features.Encounters.Triggers;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.Encounters.Default.Triggers
{
    public sealed class EncounterTriggerPropertiesBehavior :
        BaseBehavior,
        IReadOnlyEncounterTriggerPropertiesBehavior
    {
        public EncounterTriggerPropertiesBehavior(
            bool mustBeMoving,
            IIdentifier encounterId,
            IInterval encounterInterval,
            double encounterChance)
        {
            MustBeMoving = mustBeMoving;
            EncounterId = encounterId;
            EncounterInterval = encounterInterval;
            EncounterChance = encounterChance;
        }

        public bool MustBeMoving { get; }

        public IIdentifier EncounterId { get; }

        public IInterval EncounterInterval { get; }

        public double EncounterChance { get; }
    }
}
