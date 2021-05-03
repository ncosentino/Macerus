using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Enchantments.Generation;
using ProjectXyz.Api.GameObjects;

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