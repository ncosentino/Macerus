using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;

namespace Macerus.Plugins.Features.GameObjects.Items.Affixes.Default
{
    public sealed class AffixMutexGeneratorComponent : IGeneratorComponent
    {
        public AffixMutexGeneratorComponent(IIdentifier mutexKey)
        {
            MutexKey = mutexKey;
        }

        public IIdentifier MutexKey { get; }
    }
}