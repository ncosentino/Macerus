using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.GameObjects.Items.Behaviors;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Generation;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation;
using ProjectXyz.Shared.Behaviors.Filtering.Attributes;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Magic
{
    public sealed class MagicItemGenerator : IDiscoverableItemGenerator
    {
        private static readonly IFilterAttribute RequiresMagicAffix = new FilterAttribute(
            new StringIdentifier("affix-type"),
            new StringFilterAttributeValue("magic"),
            true);

        private readonly IBaseItemGenerator _baseItemGenerator;
        private readonly IEnchantmentGenerator _enchantmentGenerator;
        private readonly IHasEnchantmentsBehaviorFactory _hasEnchantmentsBehaviorFactory;
        private readonly IMagicItemNameGenerator _magicItemNameGenerator;
        private readonly IFilterContextFactory _filterContextFactory;

        public MagicItemGenerator(
            IBaseItemGenerator baseItemGenerator,
            IEnchantmentGenerator enchantmentGenerator,
            IHasEnchantmentsBehaviorFactory hasEnchantmentsBehaviorFactory,
            IMagicItemNameGenerator magicItemNameGenerator,
            IFilterContextFactory filterContextFactory)
        {
            _baseItemGenerator = baseItemGenerator;
            _enchantmentGenerator = enchantmentGenerator;
            _hasEnchantmentsBehaviorFactory = hasEnchantmentsBehaviorFactory;
            _magicItemNameGenerator = magicItemNameGenerator;
            _filterContextFactory = filterContextFactory;
        }

        public IEnumerable<IGameObject> GenerateItems(IFilterContext filterContext)
        {
            // create our new context by keeping information about attributes 
            // from our caller, but acknowledging that any that were required
            // are now fulfilled up until this point. we then cobine in the
            // newly provided attributes from the drop table.
            var generatorRequired = SupportedAttributes
                .Where(attr => attr.Required)
                .ToDictionary(x => x.Id, x => x);
            var magicItemGeneratorContext = _filterContextFactory.CreateContext(
                filterContext.MinimumCount,
                filterContext.MaximumCount,
                filterContext
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

                IHasEnchantmentsBehavior hasEnchantmentsBehavior;
                if ((hasEnchantmentsBehavior = baseItem
                    .Get<IHasEnchantmentsBehavior>()
                    .SingleOrDefault()) == null)
                {
                    hasEnchantmentsBehavior = _hasEnchantmentsBehaviorFactory.Create();

                    IHasReadOnlyEnchantmentsBehavior hasReadOnlyEnchantmentsBehavior;
                    if ((hasReadOnlyEnchantmentsBehavior = baseItem
                        .Get<IHasReadOnlyEnchantmentsBehavior>()
                        .SingleOrDefault()) != null)
                    {
                        hasEnchantmentsBehavior.AddEnchantments(hasReadOnlyEnchantmentsBehavior.Enchantments);
                    }
                }

                var attributes = magicItemGeneratorContext
                    .Attributes
                    .Where(x => !SupportedAttributes.Any(s => s.Id.Equals(x.Id)))
                    .Concat(SupportedAttributes);
                var enchantmentGeneratorContext = _filterContextFactory.CreateContext(
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

                hasEnchantmentsBehavior.AddEnchantments(enchantments);

                additionalBehaviors.Add(_magicItemNameGenerator.GenerateName(
                    baseItem,
                    enchantments));

                var baseItemBehaviorsToUse = baseItem
                    .Behaviors
                    .Where(x => !(x is IHasReadOnlyEnchantmentsBehavior));
                var magicItemBehaviors = baseItemBehaviorsToUse
                    .Concat(additionalBehaviors);
                var magicItem = new MagicItem(magicItemBehaviors);
                yield return magicItem;
            }
        }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; } = new IFilterAttribute[]
        {
            RequiresMagicAffix,
        };
    }
}