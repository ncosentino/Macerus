using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.Generation;

namespace Macerus.Plugins.Features.GameObjects.Enchantments
{
    public sealed class EnchantmentsGenerator : IDiscoverableEnchantmentGenerator
    {
        private readonly IBaseEnchantmentGenerator _baseEnchantmentGenerator;

        public EnchantmentsGenerator(IBaseEnchantmentGenerator baseEnchantmentGenerator)
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
        };
    }
}