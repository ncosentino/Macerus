using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace Macerus.Plugins.Features.Encounters
{
    public interface IEncounterCombatRewardsBehavior : IBehavior
    {
        IIdentifier DropTableId { get; }
    }
}
