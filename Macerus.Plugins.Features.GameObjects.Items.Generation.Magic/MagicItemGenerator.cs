using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.GameObjects.Items.Behaviors;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Generation;
using ProjectXyz.Api.Framework.Collections;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.GameObjects.Generation;
using ProjectXyz.Shared.Game.GameObjects.Generation.Attributes;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Magic
{
    public sealed class MagicItemGenerator : IDiscoverableItemGenerator
    {
        private static readonly IGeneratorAttribute RequiresMagicAffix = new GeneratorAttribute(
            new StringIdentifier("affix-type"),
            new StringGeneratorAttributeValue("magic"),
            true);
        private static readonly IGeneratorAttribute RequiresNormalAffix = new GeneratorAttribute(
            new StringIdentifier("affix-type"),
            new StringGeneratorAttributeValue("normal"),
            true);

        private readonly IBaseItemGenerator _baseItemGenerator;
        private readonly IEnchantmentGenerator _enchantmentGenerator;
        private readonly IItemFactory _itemFactory;
        private readonly IActiveEnchantmentManagerFactory _activeEnchantmentManagerFactory;
        private readonly IMagicItemNameGenerator _magicItemNameGenerator;

        public MagicItemGenerator(
            IBaseItemGenerator baseItemGenerator,
            IEnchantmentGenerator enchantmentGenerator,
            IItemFactory itemFactory,
            IActiveEnchantmentManagerFactory activeEnchantmentManagerFactory,
            IMagicItemNameGenerator magicItemNameGenerator)
        {
            _baseItemGenerator = baseItemGenerator;
            _enchantmentGenerator = enchantmentGenerator;
            _itemFactory = itemFactory;
            _activeEnchantmentManagerFactory = activeEnchantmentManagerFactory;
            _magicItemNameGenerator = magicItemNameGenerator;
        }

        public IEnumerable<IGameObject> GenerateItems(IGeneratorContext generatorContext)
        {
            var magicItemGeneratorContext = new GeneratorContext(
                generatorContext.MinimumGenerateCount,
                generatorContext.MaximumGenerateCount,
                generatorContext
                    .Attributes
                    .Where(x => !SupportedAttributes.Any(s => s.Id.Equals(x.Id)))
                    .Concat(SupportedAttributes)
                    .Where(x => x != RequiresMagicAffix)
                    .AppendSingle(RequiresNormalAffix));
            var baseItems = _baseItemGenerator.GenerateItems(magicItemGeneratorContext);

            foreach (var baseItem in baseItems)
            {
                // TODO: we may need to create a NEW context here to add even more specific information.
                // i.e.
                // - original context says items can be "any armor", but we
                //   generate a helm... we might want helm specific enchantments
                // - original context has a range for item level, but if our 
                //   item was at one end of that range, it might mean better or
                //    worse enchantments given the item level.

                var additionalBehaviors = new List<IBehavior>()
                {
                    new HasInventoryDisplayColor(0, 0, 255, 255),
                };

                IBuffableBehavior enchantable;
                if ((enchantable = baseItem
                    .Get<IBuffableBehavior>()
                    .SingleOrDefault()) == null)
                {
                    var activeEnchantmentManager = _activeEnchantmentManagerFactory.Create();
                    enchantable = new BuffableBehavior(activeEnchantmentManager);
                    additionalBehaviors.Add(enchantable);
                    additionalBehaviors.Add(new HasEnchantmentsBehavior(activeEnchantmentManager));
                }

                var attributes = magicItemGeneratorContext
                    .Attributes
                    .Where(x => !SupportedAttributes.Any(s => s.Id.Equals(x.Id)))
                    .Concat(SupportedAttributes);
                var enchantmentGeneratorContext = new GeneratorContext(
                    1,
                    2,
                    attributes);
                var enchantments = _enchantmentGenerator
                    .GenerateEnchantments(enchantmentGeneratorContext)
                    .ToArray();
                if (!enchantments.Any())
                {
                    throw new InvalidOperationException(
                        $"No enchantments were added to the base item.");
                }

                enchantable.AddEnchantments(enchantments);

                additionalBehaviors.Add(_magicItemNameGenerator.GenerateName(
                    baseItem,
                    enchantments));

                var magicItem = _itemFactory.Create(baseItem.Behaviors.Concat(additionalBehaviors));
                yield return magicItem;
            }
        }

        public IEnumerable<IGeneratorAttribute> SupportedAttributes { get; } = new IGeneratorAttribute[]
        {
            RequiresMagicAffix,
        };
    }
}