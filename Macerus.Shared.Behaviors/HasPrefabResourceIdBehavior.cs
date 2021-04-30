using Macerus.Api.Behaviors;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Shared.Behaviors
{
    public class HasPrefabResourceIdBehavior :
        BaseBehavior,
        IReadOnlyPrefabResourceIdBehavior
    {
        public HasPrefabResourceIdBehavior(IIdentifier prefabResourceId)
        {
            PrefabResourceId = prefabResourceId;
        }

        public IIdentifier PrefabResourceId { get; set; }
    }
}
