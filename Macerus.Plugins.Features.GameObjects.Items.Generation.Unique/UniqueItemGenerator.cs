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

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Unique
{
    public sealed class UniqueItemGenerator : IDiscoverableItemGenerator
    {
        private static readonly IFilterAttribute RequiresUniqueAffix = new FilterAttribute(
            new StringIdentifier("affix-type"),
            new StringFilterAttributeValue("unique"),
            true);

        private readonly IBaseItemGenerator _baseItemGenerator;
        private readonly IEnchantmentGenerator _enchantmentGenerator;
        private readonly IHasEnchantmentsBehaviorFactory _hasEnchantmentsBehaviorFactory;
        private readonly IFilterContextFactory _filterContextFactory;

        public UniqueItemGenerator(
            IBaseItemGenerator baseItemGenerator,
            IEnchantmentGenerator enchantmentGenerator,
            IHasEnchantmentsBehaviorFactory hasEnchantmentsBehaviorFactory,
            IFilterContextFactory filterContextFactory)
        {
            _baseItemGenerator = baseItemGenerator;
            _enchantmentGenerator = enchantmentGenerator;
            _hasEnchantmentsBehaviorFactory = hasEnchantmentsBehaviorFactory;
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
            var uniqueItemGeneratorContext = _filterContextFactory.CreateContext(
                filterContext.MinimumCount,
                filterContext.MaximumCount,
                filterContext
                    .Attributes
                    .Where(x => !generatorRequired.ContainsKey(x.Id))
                    .Select(x => x.Required
                        ? x.CopyWithRequired(false)
                        : x)
                    .Concat(generatorRequired.Values));
            var uniqueBehaviorSets = _baseItemGenerator.GenerateItems(uniqueItemGeneratorContext);
            
            foreach (var uniqueBehaviorSet in uniqueBehaviorSets)
            {
                var baseItemId = uniqueBehaviorSet
                    .GetOnly<UniqueBaseItemBehavior>()
                    .BaseItemId;

                var baseItemGeneratorContext = _filterContextFactory.CreateContext(
                    1,
                    1,
                    new FilterAttribute(
                        new StringIdentifier("item-id"),
                        new IdentifierFilterAttributeValue(baseItemId),
                        true));
                var baseItemBehaviorSet = _baseItemGenerator
                    .GenerateItems(baseItemGeneratorContext)
                    .Single();

                IHasEnchantmentsBehavior hasEnchantmentsBehavior;
                if ((hasEnchantmentsBehavior = baseItemBehaviorSet
                    .Get<IHasEnchantmentsBehavior>()
                    .SingleOrDefault()) == null)
                {
                    hasEnchantmentsBehavior = _hasEnchantmentsBehaviorFactory.Create();

                    IHasReadOnlyEnchantmentsBehavior hasReadOnlyEnchantmentsBehavior;
                    if ((hasReadOnlyEnchantmentsBehavior = baseItemBehaviorSet
                        .Get<IHasReadOnlyEnchantmentsBehavior>()
                        .SingleOrDefault()) != null)
                    {
                        hasEnchantmentsBehavior.AddEnchantments(hasReadOnlyEnchantmentsBehavior.Enchantments);
                    }
                }

                var additionalBehaviors = new List<IBehavior>()
                {
                    new HasInventoryDisplayColor(255, 215, 0, 255),
                    new HasAffixType(new StringIdentifier("unique")),
                    hasEnchantmentsBehavior,
                };

                // FIXME: we need a way to filter out dupes here
                var combinedBehaviors = uniqueBehaviorSet
                    .Behaviors
                    .Concat(baseItemBehaviorSet
                    .Behaviors
                    .Where(b => 
                        !(b is IHasReadOnlyEnchantmentsBehavior) &&
                        !uniqueBehaviorSet
                            .Behaviors
                            .Any(u => u.GetType() != b.GetType())))
                    .Concat(additionalBehaviors)
                    .Where(x => !(x is IHasInventoryDisplayName))
                    .AppendSingle(baseItemBehaviorSet
                    .Behaviors
                    .Single<IHasInventoryDisplayName>())
                    .AppendSingle(uniqueBehaviorSet
                    .Behaviors
                    .Single<IHasInventoryDisplayName>())
                    .ToList();

                

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

                var uniqueItem = new UniqueItem(combinedBehaviors);
                yield return uniqueItem;
            }
        }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; } = new IFilterAttribute[]
        {
            RequiresUniqueAffix,
        };
    }
}