using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Enchantments.Generation;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes; // FIXME: dependency on non-API
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Magic.Enchantments
{
    public sealed class MagicEnchantmentGenerator : IDiscoverableEnchantmentGenerator
    {
        private readonly IBaseEnchantmentGenerator _baseEnchantmentGenerator;

        public MagicEnchantmentGenerator(IBaseEnchantmentGenerator baseEnchantmentGenerator)
        {
            _baseEnchantmentGenerator = baseEnchantmentGenerator;
        }

        public IEnumerable<IGameObject> GenerateEnchantments(IFilterContext filterContext)
        {
            var enchantments = _baseEnchantmentGenerator.GenerateEnchantments(filterContext);
            return enchantments;
        }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; } = new IFilterAttribute[]
        {
            new FilterAttribute(
                new StringIdentifier("affix-type"),
                new StringFilterAttributeValue("magic"),
                true),
        };
    }
}
