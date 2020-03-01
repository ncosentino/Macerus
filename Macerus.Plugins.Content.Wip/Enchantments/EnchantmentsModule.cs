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
                                new HasStatGeneratorComponent(StatDefinitions.MaximumLife)
                            }),
                        new EnchantmentDefinition(
                            new[]
                            {
                                EnchantmentGeneratorAttributes.RequiresMagicAffix,
                            },
                            new IGeneratorComponent[]
                            {
                                new HasStatGeneratorComponent(StatDefinitions.MaximumMana)
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
