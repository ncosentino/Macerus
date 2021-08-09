using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.Encounters.Default
{
    public sealed class EncounterCombatRewardsBehavior :
        BaseBehavior,
        IEncounterCombatRewardsBehavior
    {
        public EncounterCombatRewardsBehavior(IEnumerable<IGameObject> loot, double experience)
        {
            Loot = loot.ToArray();
            Experience = experience;
        }

        public IReadOnlyCollection<IGameObject> Loot { get; }

        public double Experience { get; }
    }
}
