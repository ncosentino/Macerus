using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Api.Enchantments.Generation;
using ProjectXyz.Api.GameObjects.Generation;

namespace Macerus.Plugins.Features.GameObjects.Enchantments.Generation.MySql
{
    public sealed class EnchantmentDefinition : IEnchantmentDefinition
    {
        public EnchantmentDefinition(
            IEnumerable<IFilterAttribute> supportedAttributes,
            IEnumerable<IGeneratorComponent> generatorComponents)
        {
            SupportedAttributes = supportedAttributes.ToArray();
            GeneratorComponents = generatorComponents.ToArray();
        }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; }

        public IEnumerable<IGeneratorComponent> GeneratorComponents { get; }
    }
}
