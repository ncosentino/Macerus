using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace Macerus.Plugins.Features.Encounters
{
    public interface IEncounterGenerateCombatRewardsBehavior : IBehavior
    {
        IIdentifier DropTableId { get; }

        double Experience { get; }
    }
}
