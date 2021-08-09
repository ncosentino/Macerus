using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace Macerus.Plugins.Features.Encounters
{
    public interface IEncounterCombatRewardsBehavior : IBehavior
    {
        double Experience { get; }

        IReadOnlyCollection<IGameObject> Loot { get; }
    }
}
