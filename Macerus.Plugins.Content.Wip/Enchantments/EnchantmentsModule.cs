using Autofac;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Shared.Game.GameObjects.Enchantments.Generation.InMemory;
using Macerus.Plugins.Content.Wip.Stats;
using ProjectXyz.Shared.Game.GameObjects.Enchantments.Calculations;

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
                        new EnchantmentDefinition(
                            new[]
                            {
                                EnchantmentGeneratorAttributes.RequiresMagicAffix,
                            },
                            new IGeneratorComponent[]
                            {
                                new HasStatGeneratorComponent(StatDefinitions.MaximumLife),
                                new HasPrefixGeneratorComponent(Affixes.Prefixes.Lively),
                                new HasSuffixGeneratorComponent(Affixes.Suffixes.OfLife),
                                new RandomRangeExpressionGeneratorComponent(
                                    StatDefinitions.MaximumLife,
                                    "+",
                                    new CalculationPriority<int>(1),
                                    1, 10)
                            }),
                        new EnchantmentDefinition(
                            new[]
                            {
                                EnchantmentGeneratorAttributes.RequiresMagicAffix,
                            },
                            new IGeneratorComponent[]
                            {
                                new HasStatGeneratorComponent(StatDefinitions.MaximumMana),
                                new HasPrefixGeneratorComponent(Affixes.Prefixes.Magic),
                                new HasSuffixGeneratorComponent(Affixes.Suffixes.OfMana),
                                new RandomRangeExpressionGeneratorComponent(
                                    StatDefinitions.MaximumMana,
                                    "+",
                                    new CalculationPriority<int>(1),
                                    1, 10)
                            }),
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
}
