using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;

namespace Macerus.Api.Behaviors
{
    public interface IReadOnlyPrefabResourceIdBehavior : IBehavior
    {
        IIdentifier PrefabResourceId { get; }
    }
}
