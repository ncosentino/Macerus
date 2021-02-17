using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.GameObjects.Items.Behaviors;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Generation;
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

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Unique
{
    public sealed class UniqueItemGenerator : IDiscoverableItemGenerator
    {
        private static readonly IGeneratorAttribute RequiresUniqueAffix = new GeneratorAttribute(
            new StringIdentifier("affix-type"),
            new StringGeneratorAttributeValue("unique"),
            true);

        private readonly IBaseItemGenerator _baseItemGenerator;
        private readonly IEnchantmentGenerator _enchantmentGenerator;
        private readonly IItemFactory _itemFactory;
        private readonly IActiveEnchantmentManagerFactory _activeEnchantmentManagerFactory;

        public UniqueItemGenerator(
            IBaseItemGenerator baseItemGenerator,
            IEnchantmentGenerator enchantmentGenerator,
            IItemFactory itemFactory,
            IActiveEnchantmentManagerFactory activeEnchantmentManagerFactory)
        {
            _baseItemGenerator = baseItemGenerator;
            _enchantmentGenerator = enchantmentGenerator;
            _itemFactory = itemFactory;
            _activeEnchantmentManagerFactory = activeEnchantmentManagerFactory;
        }

        public IEnumerable<IGameObject> GenerateItems(IGeneratorContext generatorContext)
        {
            // create our new context by keeping information about attributes 
            // from our caller, but acknowledging that any that were required
            // are now fulfilled up until this point. we then cobine in the
            // newly provided attributes from the drop table.
            var generatorRequired = SupportedAttributes
                .Where(attr => attr.Required)
                .ToDictionary(x => x.Id, x => x);
            var magicItemGeneratorContext = new GeneratorContext(
                generatorContext.MinimumGenerateCount,
                generatorContext.MaximumGenerateCount,
                generatorContext
                    .Attributes
                    .Where(x => !generatorRequired.ContainsKey(x.Id))
                    .Select(x => x.Required
                        ? x.CopyWithRequired(false)
                        : x)
                    .Concat(generatorRequired.Values));
            var baseItems = _baseItemGenerator.GenerateItems(magicItemGeneratorContext);

            foreach (var baseItem in baseItems)
            {
                var additionalBehaviors = new List<IBehavior>()
                {
                    new HasInventoryDisplayColor(255, 215, 0, 255),
                    new HasAffixType(new StringIdentifier("unique")),
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

                // FIXME: actually implement unique item enchantments
                ////var attributes = magicItemGeneratorContext
                ////    .Attributes
                ////    .Where(x => !SupportedAttributes.Any(s => s.Id.Equals(x.Id)))
                ////    .Concat(SupportedAttributes);
                ////var enchantmentGeneratorContext = new GeneratorContext(
                ////    4,
                ////    4,
                ////    attributes);
                ////var enchantments = _enchantmentGenerator
                ////    .GenerateEnchantments(enchantmentGeneratorContext)
                ////    .ToArray();
                ////if (!enchantments.Any())
                ////{
                ////    throw new InvalidOperationException(
                ////        $"No enchantments were added to the base item.");
                ////}

                ////enchantable.AddEnchantments(enchantments);

                // FIXME: actually implement unique item names
                additionalBehaviors.Add(new HasInventoryDisplayName("This is unqiue"));

                var magicItem = _itemFactory.Create(baseItem.Behaviors.Concat(additionalBehaviors));
                yield return magicItem;
            }
        }

        public IEnumerable<IGeneratorAttribute> SupportedAttributes { get; } = new IGeneratorAttribute[]
        {
            RequiresUniqueAffix,
        };
    }
}