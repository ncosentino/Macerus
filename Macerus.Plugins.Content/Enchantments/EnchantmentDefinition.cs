using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Enchantments.Generation;
using ProjectXyz.Api.GameObjects.Generation;

namespace Macerus.Plugins.Content.Enchantments
{
    public sealed class EnchantmentDefinition : IEnchantmentDefinition
    {
        public EnchantmentDefinition(
            IEnumerable<IFilterAttribute> attributes,
            IEnumerable<IGeneratorComponent> generatorComponents)
            : this()
        {
            SupportedAttributes = attributes.ToArray();
            GeneratorComponents = generatorComponents.ToArray();
        }

        public EnchantmentDefinition() // serialization constructor
        {
        }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; set; }

        public IEnumerable<IGeneratorComponent> GeneratorComponents { get; set; }
    }
}
