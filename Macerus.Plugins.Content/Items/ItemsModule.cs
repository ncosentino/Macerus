using System;

using Autofac;

using Macerus.Api.Behaviors.Filtering;
using Macerus.Plugins.Content.Enchantments;
using Macerus.Plugins.Features.GameObjects.Items;
using Macerus.Plugins.Features.GameObjects.Items.Socketing;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.InMemory;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Content.Items
{
    public sealed class ItemsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<ItemIdentifiers>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .Register(c =>
                {
                    var itemIdentifiers = c.Resolve<IItemIdentifiers>();
                    var filterContextAmenity = c.Resolve<IFilterContextAmenity>();
                    var itemDefinitions = new[]
                    {
                        new ItemDefinition(
                            new[]
                            {
                                EnchantmentFilterAttributes.AllowsNormalMagicAndRareAffix,
                                filterContextAmenity.CreateSupportedAttribute(
                                    itemIdentifiers.ItemDefinitionIdentifier,
                                    new StringIdentifier("leather-gloves")),
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
                                EnchantmentFilterAttributes.AllowsNormalMagicAndRareAffix,
                                filterContextAmenity.CreateSupportedAttribute(
                                    itemIdentifiers.ItemDefinitionIdentifier,
                                    new StringIdentifier("cloth-hood")),
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
                                EnchantmentFilterAttributes.AllowsNormalMagicAndRareAffix,
                                filterContextAmenity.CreateSupportedAttribute(
                                    itemIdentifiers.ItemDefinitionIdentifier,
                                    new StringIdentifier("cloth-armor")),
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
                                filterContextAmenity.CreateSupportedAttribute(
                                    itemIdentifiers.ItemDefinitionIdentifier,
                                    new StringIdentifier("junk")),
                            },
                            new IGeneratorComponent[]
                            {
                                new NameGeneratorComponent("Junk"),
                                new IconGeneratorComponent(@"graphics\items\junk"),
                            }),
                        new ItemDefinition(
                            new[]
                            {
                                EnchantmentFilterAttributes.RequiresNormalAffix,
                                filterContextAmenity.CreateSupportedAttribute(
                                    itemIdentifiers.ItemDefinitionIdentifier,
                                    new StringIdentifier("ruby")),
                            },
                            new IGeneratorComponent[]
                            {
                                new NameGeneratorComponent("Ruby"),
                                new IconGeneratorComponent(@"graphics\items\gems\ruby"),
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
        }
    }
}
