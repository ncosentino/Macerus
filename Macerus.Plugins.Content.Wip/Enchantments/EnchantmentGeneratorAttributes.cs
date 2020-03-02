using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.GameObjects.Generation.Attributes;

namespace Macerus.Plugins.Content.Wip.Enchantments
{
    public static class EnchantmentGeneratorAttributes
    {
        public static IGeneratorAttribute RequiresMagicAffix { get; } = new GeneratorAttribute(
            new StringIdentifier("affix-type"),
            new StringGeneratorAttributeValue("magic"),
            true);

        public static IGeneratorAttribute RequiresNormalAffix { get; } = new GeneratorAttribute(
            new StringIdentifier("affix-type"),
            new StringGeneratorAttributeValue("normal"),
            true);

        public static IGeneratorAttribute AllowsMagicAffix { get; } = new GeneratorAttribute(
            new StringIdentifier("affix-type"),
            new StringGeneratorAttributeValue("magic"),
            false);

        public static IGeneratorAttribute AllowsNormalAffix { get; } = new GeneratorAttribute(
            new StringIdentifier("affix-type"),
            new StringGeneratorAttributeValue("normal"),
            false);
    }
}
