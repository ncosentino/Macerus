using Macerus.Plugins.Features.GameObjects.Static.Api;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Encounters.Triggers.GamObjects.Static
{
    public interface IReadOnlyEncounterTriggerPropertiesBehavior : IBehavior
    {
        double EncounterChance { get; }

        IIdentifier EncounterId { get; }

        IInterval EncounterInterval { get; }

        bool MustBeMoving { get; }

        IReadOnlyStaticGameObjectPropertiesBehavior RawProperties { get; }

        double Width { get; }

        double Height { get; }

        double X { get; }

        double Y { get; }
    }
}