using Autofac;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.GameObjects.Generation.Attributes;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Shared.Game.GameObjects.Enchantments.Generation.InMemory;
using Macerus.Plugins.Content.Wip.Stats;

namespace Macerus.Plugins.Content.Wip.Enchantments
{
    public sealed class EnchantmentsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(c =>
                {
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
                .RegisterType<ValueMapperRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
               .RegisterType<StateIdToTermRepo>()
               .AsImplementedInterfaces()
               .SingleInstance();
        }
    }

    public static class EnchantmentGeneratorAttributes
    {
        public static IGeneratorAttribute RequiresMagicAffix { get; } = new GeneratorAttribute(
            new StringIdentifier("affix-type"),
            new StringGeneratorAttributeValue("magic"),
            true);
    }
}
