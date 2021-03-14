using System;

using Autofac;

using Macerus.Plugins.Features.GameObjects.Items.Socketing;

using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes; // FIXME: dependency on non-API
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.InMemory;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.GameObjects.Items.Autofac
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
                                EnchantmentFilterAttributes.AllowsNormalAndMagicAffix,
                                new FilterAttribute(
                                    new StringIdentifier("item-id"),
                                    new IdentifierFilterAttributeValue(new StringIdentifier("leather-gloves")),
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
                                EnchantmentFilterAttributes.AllowsNormalAndMagicAffix,
                                new FilterAttribute(
                                    new StringIdentifier("item-id"),
                                    new IdentifierFilterAttributeValue(new StringIdentifier("cloth-hood")),
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
                                EnchantmentFilterAttributes.AllowsNormalAndMagicAffix,
                                new FilterAttribute(
                                    new StringIdentifier("item-id"),
                                    new IdentifierFilterAttributeValue(new StringIdentifier("cloth-armor")),
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
                                EnchantmentFilterAttributes.RequiresNormalAffix,
                                new FilterAttribute(
                                    new StringIdentifier("item-id"),
                                    new IdentifierFilterAttributeValue(new StringIdentifier("junk")),
                                    false)
                            },
                            new[]
                            {
                                new NameGeneratorComponent("Junk"),
                            }),
                        new ItemDefinition(
                            new[]
                            {
                                EnchantmentFilterAttributes.RequiresNormalAffix,
                                new FilterAttribute(
                                    new StringIdentifier("item-id"),
                                    new IdentifierFilterAttributeValue(new StringIdentifier("ruby")),
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
        }
    }
}
