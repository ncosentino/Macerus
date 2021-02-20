using System;

using Autofac;

using Macerus.Plugins.Content.Wip.Enchantments;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Framework;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.InMemory;
using ProjectXyz.Shared.Behaviors.Filtering.Attributes;
using ProjectXyz.Shared.Framework;

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
                                EnchantmentFilterAttributes.AllowsNormalAndMagicAffix,
                                new FilterAttribute(
                                    new StringIdentifier("item-id"),
                                    new IdentifierFilterAttributeValue(new StringIdentifier("leather-gloves")),
                                    false)
                            },
                            new IFilterComponent[]
                            {
                               new NameFilterComponent("Leather Gloves"),
                               new IconFilterComponent(@"graphics\items\gloves\leather gloves"),
                               new EquippableFilterComponent(new[] { new StringIdentifier("hands") }),
                               new SocketFilterComponent(new[]
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
                            new IFilterComponent[]
                            {
                                new NameFilterComponent("Cloth Hood"),
                                new IconFilterComponent(@"graphics\items\helms\hood"),
                                new EquippableFilterComponent(new[] { new StringIdentifier("head") }),
                                new SocketFilterComponent(new[]
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
                            new IFilterComponent[]
                            {
                                new NameFilterComponent("Cloth Armor"),
                                new IconFilterComponent(@"graphics\items\body\cloth_armor"),
                                new EquippableFilterComponent(new[] { new StringIdentifier("body") }),
                                new SocketFilterComponent(new[]
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
                                new NameFilterComponent("Junk"),
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
                            new IFilterComponent[]
                            {
                                new NameFilterComponent("Ruby"),
                                new CanFitSocketFilterComponent(new StringIdentifier("gem"), 1),
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
                .RegisterType<NameFilterComponentToBehaviorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<EquippableFilterComponentToBehaviorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<IconFilterComponentToBehaviorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<SocketFilterComponentToBehaviorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<CanFitSocketFilterComponentToBehaviorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
