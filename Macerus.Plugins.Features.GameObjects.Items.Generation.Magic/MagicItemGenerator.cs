using System.Collections.Generic;
using ProjectXyz.Api.Enchantments.Generation;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.GameObjects.Generation.Attributes;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Magic
{
    public sealed class MagicItemGenerator : IItemGenerator
    {
        private readonly IBaseItemGenerator _baseItemGenerator;
        private readonly IEnchantmentGenerator _enchantmentGenerator;

        public MagicItemGenerator(
            IBaseItemGenerator baseItemGenerator,
            IEnchantmentGenerator enchantmentGenerator)
        {
            _baseItemGenerator = baseItemGenerator;
            _enchantmentGenerator = enchantmentGenerator;
        }

        public IEnumerable<IGameObject> GenerateItems(IGeneratorContext generatorContext)
        {
            var items = _baseItemGenerator.GenerateItems(generatorContext);

            foreach (var item in items)
            {
                // TODO: we may need to create a NEW context here to add even more specific information.
                // i.e.
                // - original context says items can be "any armor", but we
                //   generate a helm... we might want helm specific enchantments
                // - original context has a range for item level, but if our 
                //   item was at one end of that range, it might mean better or
                //    worse enchantments given the item level.

                var enchantments = _enchantmentGenerator.GenerateEnchantments(generatorContext);

                // TODO: do we just assume that items MUST have this behavior on them?
                item
                    .GetOnly<IEnchantableBehavior>()
                    .Enchant(enchantments);

                yield return item;
            }
        }

        public IEnumerable<IGeneratorAttribute> SupportedAttributes { get; } = new IGeneratorAttribute[]
        {
            new GeneratorAttribute(
                new StringIdentifier("affix-type"),
                new StringGeneratorAttributeValue("magic"),
                false),
        };
    }
}