using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.Encounters.Default
{
    public sealed class EncounterGenerateCombatRewardsBehavior :
        BaseBehavior,
        IEncounterGenerateCombatRewardsBehavior
    {
        public EncounterGenerateCombatRewardsBehavior(
            IIdentifier dropTableId,
            double experience)
        {
            DropTableId = dropTableId;
            Experience = experience;
        }

        public IIdentifier DropTableId { get; }

        public double Experience { get; }
    }
}
