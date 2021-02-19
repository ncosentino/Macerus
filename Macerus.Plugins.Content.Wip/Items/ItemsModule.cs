using System;

using Autofac;

using Macerus.Plugins.Content.Wip.Enchantments;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.InMemory;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.GameObjects.Generation.Attributes;

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
                                EnchantmentGeneratorAttributes.AllowsNormalAndMagicAffix,
                                new GeneratorAttribute(
                                    new StringIdentifier("item-id"),
                                    new IdentifierGeneratorAttributeValue(new StringIdentifier("leather-gloves")),
                                    false)
                            },
                            new IGeneratorComponent[]
                            {
                               new NameGeneratorComponent("Leather Gloves"),
                               new IconGeneratorComponent(@"graphics\items\gloves\leather gloves"),
                               new EquippableGeneratorComponent(new[] { new StringIdentifier("hands") }),
                               new SocketGeneratorComponent(new[]
                                {
                                    KeyValuePair.Create((IIdentifier)new StringIdentifier("gem"), Tuple.Create(0, 4)),
                                })
                            }),
                        new ItemDefinition(
                            new[]
                            {
                                EnchantmentGeneratorAttributes.AllowsNormalAndMagicAffix,
                                new GeneratorAttribute(
                                    new StringIdentifier("item-id"),
                                    new IdentifierGeneratorAttributeValue(new StringIdentifier("cloth-hood")),
                                    false)
                            },
                            new IGeneratorComponent[]
                            {
                                new NameGeneratorComponent("Cloth Hood"),
                                new IconGeneratorComponent(@"graphics\items\helms\hood"),
                                new EquippableGeneratorComponent(new[] { new StringIdentifier("head") }),
                                new SocketGeneratorComponent(new[]
                                {
                                    KeyValuePair.Create((IIdentifier)new StringIdentifier("gem"), Tuple.Create(0, 4)),
                                })
                            }),
                        new ItemDefinition(
                            new[]
                            {
                                EnchantmentGeneratorAttributes.AllowsNormalAndMagicAffix,
                                new GeneratorAttribute(
                                    new StringIdentifier("item-id"),
                                    new IdentifierGeneratorAttributeValue(new StringIdentifier("cloth-armor")),
                                    false)
                            },
                            new IGeneratorComponent[]
                            {
                                new NameGeneratorComponent("Cloth Armor"),
                                new IconGeneratorComponent(@"graphics\items\body\cloth_armor"),
                                new EquippableGeneratorComponent(new[] { new StringIdentifier("body") }),
                                new SocketGeneratorComponent(new[]
                                {
                                    KeyValuePair.Create((IIdentifier)new StringIdentifier("gem"), Tuple.Create(0, 6)),
                                })
                            }),
                        new ItemDefinition(
                            new[]
                            {
                                EnchantmentGeneratorAttributes.RequiresNormalAffix,
                                new GeneratorAttribute(
                                    new StringIdentifier("item-id"),
                                    new IdentifierGeneratorAttributeValue(new StringIdentifier("junk")),
                                    false)
                            },
                            new[]
                            {
                                new NameGeneratorComponent("Junk"),
                            }),
                        new ItemDefinition(
                            new[]
                            {
                                EnchantmentGeneratorAttributes.RequiresNormalAffix,
                                new GeneratorAttribute(
                                    new StringIdentifier("item-id"),
                                    new IdentifierGeneratorAttributeValue(new StringIdentifier("ruby")),
                                    false)
                            },
                            new IGeneratorComponent[]
                            {
                                new NameGeneratorComponent("Ruby"),
                                new CanFitSocketGeneratorComponent(new StringIdentifier("gem"), 1),
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
            builder
                .RegisterType<SocketGeneratorComponentToBehaviorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<CanFitSocketGeneratorComponentToBehaviorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
