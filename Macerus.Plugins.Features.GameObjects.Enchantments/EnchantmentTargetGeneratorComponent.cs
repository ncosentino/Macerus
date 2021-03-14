using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;

namespace Macerus.Plugins.Features.GameObjects.Enchantments
{
    public sealed class EnchantmentTargetGeneratorComponent : IGeneratorComponent
    {
        public EnchantmentTargetGeneratorComponent(IIdentifier target)
        {
            Target = target;
        }

        public IIdentifier Target { get; set; }
    }
}
