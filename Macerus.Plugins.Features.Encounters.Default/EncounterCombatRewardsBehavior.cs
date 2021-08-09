using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.Encounters.Default
{
    public sealed class EncounterCombatRewardsBehavior :
        BaseBehavior,
        IEncounterCombatRewardsBehavior
    {
        public EncounterCombatRewardsBehavior(IIdentifier dropTableId)
        {
            DropTableId = dropTableId;
        }

        public IIdentifier DropTableId { get; }
    }
}
