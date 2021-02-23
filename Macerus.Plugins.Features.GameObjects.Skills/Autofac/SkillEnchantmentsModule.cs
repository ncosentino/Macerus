using System.Collections.Generic;
using System.Linq;

using Autofac;

using Macerus.Plugins.Features.GameObjects.Enchantments;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Enchantments.Generation;
using ProjectXyz.Api.Framework;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes; // FIXME: dependency on non-API
using ProjectXyz.Plugins.Features.Enchantments.Generation.InMemory;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.GameObjects.Skills.Autofac
{
    public sealed class MagicEnchantmentsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(c =>
                {
                    var enchantmentTemplate = new EnchantmentTemplate(c.Resolve<ICalculationPriorityFactory>());
                    var enchantmentDefinitions = new[]
                    {
                        enchantmentTemplate.CreateSkillEnchantment(
                            new StringIdentifier("green-glow"),
                            new IntIdentifier(8), // green light radius
                            1,
                            1),
                    };
                    var repository = new InMemoryEnchantmentDefinitionRepository(
                        c.Resolve<IAttributeFilterer>(),
                        enchantmentDefinitions);
                    return repository;
                })
                .SingleInstance()
                .AsImplementedInterfaces();
        }
    }

    public sealed class EnchantmentTemplate
    {
        private readonly ICalculationPriorityFactory _calculationPriorityFactory;

        public EnchantmentTemplate(ICalculationPriorityFactory calculationPriorityFactory)
        {
            _calculationPriorityFactory = calculationPriorityFactory;
        }

        public IEnchantmentDefinition CreateSkillEnchantment(
            IIdentifier skillDefinitionId,
            IIdentifier statDefinitionId,
            double minValue,
            double maxValue)
        {
            var enchantmentDefinition = new EnchantmentDefinition(
                new[]
                {
                    new FilterAttribute(
                        new StringIdentifier("skill-definition-id"),
                        new IdentifierFilterAttributeValue(skillDefinitionId),
                        true),
                },
                new IFilterComponent[]
                {
                    new EnchantmentTargetFilterComponent(new StringIdentifier("self")),
                    new HasStatFilterComponent(statDefinitionId),
                    new RandomRangeExpressionFilterComponent(
                        statDefinitionId,
                        "+",
                        _calculationPriorityFactory.Create<int>(1),
                        minValue, maxValue)
                });
            return enchantmentDefinition;
        }

        public sealed class EnchantmentDefinition : IEnchantmentDefinition
        {
            public EnchantmentDefinition(
                IEnumerable<IFilterAttribute> attributes,
                IEnumerable<IFilterComponent> filterComponents)
                : this()
            {
                SupportedAttributes = attributes.ToArray();
                FilterComponents = filterComponents.ToArray();
            }

            public EnchantmentDefinition() // serialization constructor
            {
            }

            public IEnumerable<IFilterAttribute> SupportedAttributes { get; set; }

            public IEnumerable<IFilterComponent> FilterComponents { get; set; }
        }
    }
}
