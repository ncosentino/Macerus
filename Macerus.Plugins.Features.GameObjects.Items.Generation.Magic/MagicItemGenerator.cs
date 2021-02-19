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
using ProjectXyz.Shared.Game.GameObjects.Generation.Attributes;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Magic
{
    public sealed class MagicItemGenerator : IDiscoverableItemGenerator
    {
        private static readonly IGeneratorAttribute RequiresMagicAffix = new GeneratorAttribute(
            new StringIdentifier("affix-type"),
            new StringGeneratorAttributeValue("magic"),
            true);

        private readonly IBaseItemGenerator _baseItemGenerator;
        private readonly IEnchantmentGenerator _enchantmentGenerator;
        private readonly IActiveEnchantmentManagerFactory _activeEnchantmentManagerFactory;
        private readonly IMagicItemNameGenerator _magicItemNameGenerator;
        private readonly IGeneratorContextFactory _generatorContextFactory;

        public MagicItemGenerator(
            IBaseItemGenerator baseItemGenerator,
            IEnchantmentGenerator enchantmentGenerator,
            IActiveEnchantmentManagerFactory activeEnchantmentManagerFactory,
            IMagicItemNameGenerator magicItemNameGenerator,
            IGeneratorContextFactory generatorContextFactory)
        {
            _baseItemGenerator = baseItemGenerator;
            _enchantmentGenerator = enchantmentGenerator;
            _activeEnchantmentManagerFactory = activeEnchantmentManagerFactory;
            _magicItemNameGenerator = magicItemNameGenerator;
            _generatorContextFactory = generatorContextFactory;
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
            var magicItemGeneratorContext = _generatorContextFactory.CreateGeneratorContext(
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
                    new HasAffixType(new StringIdentifier("magic")),
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
                var enchantmentGeneratorContext = _generatorContextFactory.CreateGeneratorContext(
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

                var magicItem = new MagicItem(baseItem.Behaviors.Concat(additionalBehaviors));
                yield return magicItem;
            }
        }

        public IEnumerable<IGeneratorAttribute> SupportedAttributes { get; } = new IGeneratorAttribute[]
        {
            RequiresMagicAffix,
        };
    }
}