using System.Collections.Generic;
using System.Linq;
using Macerus.Plugins.Features.GameObjects.Enchantments.Generation.Magic;
using Macerus.Plugins.Features.GameObjects.Items.Behaviors;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Generation;
using ProjectXyz.Api.Framework;
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
        private readonly IRandomNumberGenerator _randomNumberGenerator;
        private readonly IBaseItemGenerator _baseItemGenerator;
        private readonly IEnchantmentGenerator _enchantmentGenerator;
        private readonly IItemFactory _itemFactory;
        private readonly IActiveEnchantmentManagerFactory _activeEnchantmentManagerFactory;

        public MagicItemGenerator(
            IRandomNumberGenerator randomNumberGenerator,
            IBaseItemGenerator baseItemGenerator,
            IEnchantmentGenerator enchantmentGenerator,
            IItemFactory itemFactory,
            IActiveEnchantmentManagerFactory activeEnchantmentManagerFactory)
        {
            _randomNumberGenerator = randomNumberGenerator;
            _baseItemGenerator = baseItemGenerator;
            _enchantmentGenerator = enchantmentGenerator;
            _itemFactory = itemFactory;
            _activeEnchantmentManagerFactory = activeEnchantmentManagerFactory;
        }

        public IEnumerable<IGameObject> GenerateItems(IGeneratorContext generatorContext)
        {
            var magicItemGeneratorContext = new GeneratorContext(
                generatorContext.MinimumGenerateCount,
                generatorContext.MaximumGenerateCount,
                generatorContext
                    .Attributes
                    .Where(x => !SupportedAttributes.Any(s => s.Id.Equals(x.Id)))
                    .Concat(SupportedAttributes));
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

                var enchantmentGeneratorContext = new GeneratorContext(
                    1,
                    2,
                    magicItemGeneratorContext
                        .Attributes
                        .Where(x => !SupportedAttributes.Any(s => s.Id.Equals(x.Id)))
                        .Concat(SupportedAttributes));

                var enchantments = _enchantmentGenerator
                    .GenerateEnchantments(enchantmentGeneratorContext)
                    .ToArray();
                enchantable.AddEnchantments(enchantments);

                additionalBehaviors.Add(GenerateName(
                    baseItem,
                    enchantments));

                var magicItem = _itemFactory.Create(baseItem.Behaviors.Concat(additionalBehaviors));
                yield return magicItem;
            }
        }

        public IEnumerable<IGeneratorAttribute> SupportedAttributes { get; } = new IGeneratorAttribute[]
        {
            new GeneratorAttribute(
                new StringIdentifier("affix-type"),
                new StringGeneratorAttributeValue("magic"),
                true),
        };

        private IHasInventoryDisplayName GenerateName(
            IGameObject baseItem,
            IReadOnlyCollection<IEnchantment> enchantments)
        {
            // FIXME: make this a bit more robust. it assumes every enchantment
            // MUST exist as a prefix AND a suffix, and we can pick and choose
            // at will. what if we want to change that assumption? what if some
            // enchantments can only exist as a prefix *OR* a suffix?
            var prefixes = enchantments
                .ToDictionary(
                    x => x,
                    x => x.GetOnly<IHasPrefixBehavior>());
            var suffixes = enchantments
                .ToDictionary(
                    x => x,
                    x => x.GetOnly<IHasSuffixBehavior>());
            var baseDisplayName = baseItem.GetOnly<IHasInventoryDisplayName>();
            if (enchantments.Count == 1)
            {
                // TODO: these are identifiers for the prefix and NOT the string
                // value... we need to look this up!
                return new HasInventoryDisplayName(_randomNumberGenerator.NextDouble() >= 0.5
                    ? $"{baseDisplayName.DisplayName} {suffixes.Single().Value.SuffixId}"
                    : $"{prefixes.Single().Value.PrefixId} {baseDisplayName.DisplayName}");
            }

            var prefixEnchantmentIndex = _randomNumberGenerator.NextInRange(0, 1);
            var suffixEnchantmentIndex = prefixEnchantmentIndex == 0
                ? 1
                : 0;
            var prefix = prefixes[prefixEnchantmentIndex == 0 ? enchantments.First() : enchantments.Last()];
            var suffix = suffixes[suffixEnchantmentIndex == 0 ? enchantments.First() : enchantments.Last()];
            
            // TODO: these are identifiers for the prefix and NOT the string
            // value... we need to look this up!
            return new HasInventoryDisplayName($"{prefix.PrefixId} {baseDisplayName.DisplayName} {suffix.SuffixId}");
        }
    }
}