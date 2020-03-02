using Autofac;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.InMemory;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Framework.Autofac;
using Macerus.Plugins.Content.Wip.Enchantments;

namespace Macerus.Plugins.Content.Wip.Items
{
    public sealed class ItemsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(c =>
                {
                    var itemDefinitions = new[]
                    {
                        new ItemDefinition(
                            new[]
                            {
                                EnchantmentGeneratorAttributes.AllowsMagicAffix,
                            },
                            new IGeneratorComponent[]
                            {
                               new NameGeneratorComponent("Gloves"),
                               new IconGeneratorComponent(@"graphics\items\gloves\leather gloves"),
                               new EquippableGeneratorComponent(new[] { new StringIdentifier("hands") }),
                            }),
                        new ItemDefinition(
                            new[]
                            {
                                EnchantmentGeneratorAttributes.AllowsMagicAffix,
                            },
                            new IGeneratorComponent[]
                            {
                                new NameGeneratorComponent("Helm"),
                                new EquippableGeneratorComponent(new[] { new StringIdentifier("head") }),
                            }),
                        new ItemDefinition(
                            new[]
                            {
                                EnchantmentGeneratorAttributes.RequiresNormalAffix
                            },
                            new[]
                            {
                                new NameGeneratorComponent("Junk"),
                            }),
                    };
                    var itemDefinitionRepository = new InMemoryItemDefinitionRepository(
                        c.Resolve<IAttributeFilterer>(),
                        itemDefinitions);
                    return itemDefinitionRepository;
                })
                .SingleInstance()
                .AsImplementedInterfaces();
            builder
                .RegisterType<NameGeneratorComponentToBehaviorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<EquippableGeneratorComponentToBehaviorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<IconGeneratorComponentToBehaviorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
