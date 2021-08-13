using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.GameObjects.Items.Behaviors;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.Filtering.Default.Attributes; // FIXME: dependency on non-API
using ProjectXyz.Plugins.Features.GameObjects.Enchantments;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation;
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
        private readonly IGameObjectFactory _gameObjectFactory;

        public UniqueItemGenerator(
            IBaseItemGenerator baseItemGenerator,
            IEnchantmentGenerator enchantmentGenerator,
            IHasEnchantmentsBehaviorFactory hasEnchantmentsBehaviorFactory,
            IFilterContextFactory filterContextFactory,
            IGameObjectFactory gameObjectFactory)
        {
            _baseItemGenerator = baseItemGenerator;
            _enchantmentGenerator = enchantmentGenerator;
            _hasEnchantmentsBehaviorFactory = hasEnchantmentsBehaviorFactory;
            _filterContextFactory = filterContextFactory;
            _gameObjectFactory = gameObjectFactory;
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

                    IReadOnlyHasEnchantmentsBehavior hasReadOnlyEnchantmentsBehavior;
                    if ((hasReadOnlyEnchantmentsBehavior = baseItemBehaviorSet
                        .Get<IReadOnlyHasEnchantmentsBehavior>()
                        .SingleOrDefault()) != null)
                    {
                        hasEnchantmentsBehavior
                            .AddEnchantmentsAsync(hasReadOnlyEnchantmentsBehavior.Enchantments)
                            .Wait();
                    }
                }

                var additionalBehaviors = new List<IBehavior>()
                {
                    new HasInventoryBackgroundColor(0xd0, 0x81, 0x06),
                    new HasAffixType(new StringIdentifier("unique")),
                    hasEnchantmentsBehavior,
                };

                // FIXME: we need a way to filter out dupes here
                var combinedBehaviors = uniqueBehaviorSet
                    .Behaviors
                    .Concat(baseItemBehaviorSet
                    .Behaviors
                    .Where(b => 
                        !(b is IReadOnlyHasEnchantmentsBehavior) &&
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

                var uniqueItem = _gameObjectFactory.Create(combinedBehaviors);
                yield return uniqueItem;
            }
        }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; } = new IFilterAttribute[]
        {
            RequiresUniqueAffix,
        };
    }
}