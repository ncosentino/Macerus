
using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Items.Affixes.Default
{
    public sealed class AffixMutexBehavior : 
        BaseBehavior,
        IAffixMutexBehavior
    {
        public AffixMutexBehavior(IIdentifier mutexKey)
        {
            MutexKey = mutexKey;
        }

        public IIdentifier MutexKey { get; }
    }
}