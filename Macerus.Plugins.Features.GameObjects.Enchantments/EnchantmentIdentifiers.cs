
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.GameObjects.Enchantments
{
    public sealed class EnchantmentIdentifiers : IEnchantmentIdentifiers
    {
        public IIdentifier EnchantmentDefinitionId { get; } = new StringIdentifier("id");
    }
}
