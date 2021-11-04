using System.Collections.Generic;
using System.Linq;

using Macerus.Api.Behaviors.Filtering;
using Macerus.Plugins.Features.GameObjects.Items;
using Macerus.Plugins.Features.GameObjects.Items.Affixes.Api;
using Macerus.Plugins.Features.GameObjects.Items.Affixes.Default;
using Macerus.Plugins.Features.GameObjects.Items.Generation.Magic;

using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.CommonBehaviors.Filtering;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.Filtering.Default.Attributes; // FIXME: dependency on non-API
using ProjectXyz.Shared.Framework;

namespace Macerus.Content.Affixes
{
    public sealed class AffixTemplate
    {
        private readonly IFilterContextAmenity _filterContextAmenity;
        private readonly IEnchantmentIdentifiers _enchantmentIdentifiers;

        public AffixTemplate(
            IFilterContextAmenity filterContextAmenity,
            IEnchantmentIdentifiers enchantmentIdentifiers)
        {
            _filterContextAmenity = filterContextAmenity;
            _enchantmentIdentifiers = enchantmentIdentifiers;
        }

        public IAffixDefinition CreateMagicAffix(
            IIdentifier affixId,
            double minLevel,
            double maxLevel,
            IIdentifier prefixStringResourceId,
            IIdentifier suffixStringResourceId,
            IIdentifier mutexKey,
            IReadOnlyCollection<IIdentifier> requiredItemTags,
            params IIdentifier[] enchantmentDefinitionIds) =>
            CreateAffix(
                affixId,
                AffixFilterAttributes.RequiresMagicAffix,
                minLevel,
                maxLevel,
                mutexKey,
                requiredItemTags,
                enchantmentDefinitionIds,
                new IGeneratorComponent[]
                {
                    new MagicItemAffixGeneratorComponent(
                        prefixStringResourceId,
                        suffixStringResourceId),
                });

        public IAffixDefinition CreateRareAffix(
            IIdentifier affixId,
            double minLevel,
            double maxLevel,
            IIdentifier mutexKey,
            IReadOnlyCollection<IIdentifier> requiredItemTags,
            params IIdentifier[] enchantmentDefinitionIds) =>
            CreateAffix(
                affixId,
                AffixFilterAttributes.RequiresRareAffix,
                minLevel,
                maxLevel,
                mutexKey,
                requiredItemTags,
                enchantmentDefinitionIds,
                Enumerable.Empty<IGeneratorComponent>());

        private IAffixDefinition CreateAffix(
            IIdentifier affixId,
            IFilterAttribute affixTypeFilterAttribute, 
            double minLevel,
            double maxLevel,
            IIdentifier mutexKey,
            IReadOnlyCollection<IIdentifier> requiredItemTags,
            IReadOnlyCollection<IIdentifier> enchantmentDefinitionIds,
            IEnumerable<IGeneratorComponent> extraComponents)
        {
            var filters = new List<IFilterAttribute>()
            {
                affixTypeFilterAttribute,
                new FilterAttribute(
                    new StringIdentifier("affix-mutex-key"),
                    new IdentifierFilterAttributeValue(mutexKey),
                    false),
                new FilterAttribute(
                    new StringIdentifier("item-level"),
                    new RangeFilterAttributeValue(minLevel, maxLevel),
                    true),
                _filterContextAmenity.CreateSupportedAttribute(
                    new StringIdentifier("affix-id"),
                    affixId),
            };

            if (requiredItemTags.Any())
            {
                filters.Add(_filterContextAmenity.CreateRequiredAttribute(
                    new StringIdentifier("tags"),
                    new AnyTagsFilter(requiredItemTags)));
            }

            var affixDefinition = new AffixDefinition(
                affixId,
                new IGeneratorComponent[]
                {
                    new EnchantmentsGeneratorComponent(enchantmentDefinitionIds.Select(enchantmentDefinitionId => new IFilterAttribute[] 
                    {
                        new FilterAttribute(
                            _enchantmentIdentifiers.EnchantmentDefinitionId,
                            new IdentifierFilterAttributeValue(enchantmentDefinitionId),
                            true),
                    })),
                    new AffixMutexGeneratorComponent(mutexKey),
                }.Concat(extraComponents),
                filters);
            return affixDefinition;
        }
    }
}
