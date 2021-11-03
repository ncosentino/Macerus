
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Items.Affixes.Default
{
    public interface IAffixMutexBehavior : IBehavior
    {
        IIdentifier MutexKey { get; }
    }
}