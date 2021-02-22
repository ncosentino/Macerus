using System.Collections.Generic;
using System.Linq;

using Autofac;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Enchantments.Generation;
using ProjectXyz.Api.Framework;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Enchantments.Generation.InMemory;
using ProjectXyz.Shared.Behaviors.Filtering.Attributes;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.GameObjects.Enchantments.Calculations;

namespace Macerus.Plugins.Features.GameObjects.Skills.Autofac
{
    public sealed class MagicEnchantmentsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            //builder
            //    .Register(c =>
            //    {
            //        var enchantmentDefinitions = new[]
            //        {
            //            EnchantmentTemplate.CreateMagicRangeEnchantment(
            //                new IntIdentifier(1), // max life
            //                Affixes.Prefixes.Lively,
            //                Affixes.Suffixes.OfLife,
            //                1,
            //                15,
            //                0,
            //                20),
            //        };
            //        var repository = new InMemoryEnchantmentDefinitionRepository(
            //            c.Resolve<IAttributeFilterer>(),
            //            enchantmentDefinitions);
            //        return repository;
            //    })
            //    .SingleInstance()
            //    .AsImplementedInterfaces();
        }
    }

    ////public static class EnchantmentTemplate
    ////{
    ////    public static IEnchantmentDefinition CreateMagicRangeEnchantment(
    ////        IIdentifier statDefinitionId,
    ////        IIdentifier prefixId,
    ////        IIdentifier suffixId,
    ////        double minValue,
    ////        double maxValue,
    ////        double minLevel,
    ////        double maxLevel)
    ////    {
    ////        var enchantmentDefinition = new EnchantmentDefinition(
    ////            new[]
    ////            {
    ////                EnchantmentFilterAttributes.RequiresMagicAffix,
    ////                new FilterAttribute(
    ////                    new StringIdentifier("item-level"),
    ////                    new RangeFilterAttributeValue(minLevel, maxLevel),
    ////                    true),
    ////            },
    ////            new IFilterComponent[]
    ////            {
    ////                new HasStatFilterComponent(statDefinitionId),
    ////                new HasPrefixFilterComponent(prefixId),
    ////                new HasSuffixFilterComponent(suffixId),
    ////                new RandomRangeExpressionFilterComponent(
    ////                    statDefinitionId,
    ////                    "+",
    ////                    new CalculationPriority<int>(1),
    ////                    minValue, maxValue)
    ////            });
    ////        return enchantmentDefinition;
    ////    }

    ////    public sealed class EnchantmentDefinition : IEnchantmentDefinition
    ////    {
    ////        public EnchantmentDefinition(
    ////            IEnumerable<IFilterAttribute> attributes,
    ////            IEnumerable<IFilterComponent> filterComponents)
    ////            : this()
    ////        {
    ////            SupportedAttributes = attributes.ToArray();
    ////            FilterComponents = filterComponents.ToArray();
    ////        }

    ////        public EnchantmentDefinition() // serialization constructor
    ////        {
    ////        }

    ////        public IEnumerable<IFilterAttribute> SupportedAttributes { get; set; }

    ////        public IEnumerable<IFilterComponent> FilterComponents { get; set; }
    ////    }
    ////}
}
