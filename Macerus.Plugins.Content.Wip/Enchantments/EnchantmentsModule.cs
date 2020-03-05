using Autofac;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Shared.Game.GameObjects.Enchantments.Generation.InMemory;
using Macerus.Plugins.Content.Wip.Stats;
using ProjectXyz.Shared.Game.GameObjects.Enchantments.Calculations;
using ProjectXyz.Shared.Game.GameObjects.Generation.Attributes;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Api.Enchantments.Generation;
using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Content.Wip.Enchantments
{
    public sealed class EnchantmentsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(c =>
                {
                    // FIXME: it's totally a smell that we have a stat
                    // definition ID per enchantment when we have things like
                    // expressions that are responsible for that stat ID.
                    // notice the double reference to a stat ID per
                    // enchantment (but i guess this is because the expression
                    // must get assigned to only one stat id).
                    // See this class for related comments:
                    // ProjectXyz.Shared.Game.GameObjects.Enchantments.EnchantmentFactory
                    var enchantmentDefinitions = new[]
                    {
                        EnchantmentTemplate.CreateMagicRangeEnchantment(
                            StatDefinitions.MaximumLife,
                            Affixes.Prefixes.Lively,
                            Affixes.Suffixes.OfLife,
                            1,
                            15,
                            0,
                            20),
                        EnchantmentTemplate.CreateMagicRangeEnchantment(
                            StatDefinitions.MaximumLife,
                            Affixes.Prefixes.Hearty,
                            Affixes.Suffixes.OfHeart,
                            16,
                            50,
                            10,
                            30),
                        EnchantmentTemplate.CreateMagicRangeEnchantment(
                            StatDefinitions.MaximumMana,
                            Affixes.Prefixes.Magic,
                            Affixes.Suffixes.OfMana,
                            1,
                            15,
                            0,
                            10),
                    };
                    var repository = new InMemoryEnchantmentDefinitionRepository(
                        c.Resolve<IAttributeFilterer>(),
                        enchantmentDefinitions);
                    return repository;
                })
                .SingleInstance()
                .AsImplementedInterfaces();
            builder
                .RegisterType<HasStatGeneratorComponentToBehaviorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<HasPrefixGeneratorComponentToBehaviorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<HasSuffixGeneratorComponentToBehaviorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<RandomRangeExpressionGeneratorComponentToBehaviorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ValueMapperRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
               .RegisterType<StateIdToTermRepo>()
               .AsImplementedInterfaces()
               .SingleInstance();
        }
    }

    public static class EnchantmentTemplate
    {
        public static IEnchantmentDefinition CreateMagicRangeEnchantment(
            IIdentifier statDefinitionId,
            IIdentifier prefixId,
            IIdentifier suffixId,
            double minValue,
            double maxValue,
            double minLevel,
            double maxLevel)
        {
            var enchantmentDefinition = new EnchantmentDefinition(
                new[]
                {
                    EnchantmentGeneratorAttributes.RequiresMagicAffix,
                    new GeneratorAttribute(
                        new StringIdentifier("item-level"),
                        new RangeGeneratorAttributeValue(minLevel, maxLevel),
                        true),
                },
                new IGeneratorComponent[]
                {
                    new HasStatGeneratorComponent(statDefinitionId),
                    new HasPrefixGeneratorComponent(prefixId),
                    new HasSuffixGeneratorComponent(suffixId),
                    new RandomRangeExpressionGeneratorComponent(
                        statDefinitionId,
                        "+",
                        new CalculationPriority<int>(1),
                        minValue, maxValue)
                });
            return enchantmentDefinition;
        }
    }
}
