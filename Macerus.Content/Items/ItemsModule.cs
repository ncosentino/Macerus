﻿using System;
using System.Collections.Generic;

using Autofac;

using Macerus.Api.Behaviors.Filtering;
using Macerus.Content.Affixes;
using Macerus.Plugins.Features.GameObjects.Items;
using Macerus.Plugins.Features.GameObjects.Items.Socketing;
using Macerus.Plugins.Features.Inventory.Default.HoverCards;
using Macerus.Shared.Behaviors;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Items;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.InMemory;
using ProjectXyz.Shared.Framework;

namespace Macerus.Content.Items
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
               .Register(x =>
               {
                   var loadOrder = new HoverCardPartConverterLoadOrder(new Dictionary<Type, int>()
                   {
                       [typeof(NameHoverCardPartConverter)] = int.MinValue,
                       [typeof(BaseStatsHoverCardPartConverter)] = int.MinValue + 1,
                   });
                   return loadOrder;
               })
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
                                filterContextAmenity.CreateRequiredAttribute(
                                    itemIdentifiers.ItemDefinitionIdentifier,
                                    new StringIdentifier("gold")),
                            },
                            new IGeneratorComponent[]
                            {
                                new NameGeneratorComponent("Gold"),
                                new IconGeneratorComponent(new StringIdentifier(@"graphics\items\gold")),
                            }),
                        new ItemDefinition(
                            new[]
                            {
                                AffixFilterAttributes.AllowsNormalMagicAndRareAffix,
                                filterContextAmenity.CreateSupportedAttribute(
                                    itemIdentifiers.ItemDefinitionIdentifier,
                                    new StringIdentifier("leather-gloves")),
                            },
                            new IGeneratorComponent[]
                            {
                                new NameGeneratorComponent("Leather Gloves"),
                                new IconGeneratorComponent(new StringIdentifier(@"graphics\items\gloves\leather gloves")),
                                new EquippableGeneratorComponent(new[] { new StringIdentifier("hands") }),
                                new SocketGeneratorComponent(new[]
                                {
                                    KeyValuePair.Create((IIdentifier)new StringIdentifier("gem"), Tuple.Create(0, 4)),
                                }),
                                new HasStatsGeneratorComponent(new Dictionary<IIdentifier, double>()
                                {
                                    [new StringIdentifier("armor")] = 1, // FIXME: just for testing
                                }),
                            }),
                        new ItemDefinition(
                            new[]
                            {
                                AffixFilterAttributes.AllowsNormalMagicAndRareAffix,
                                filterContextAmenity.CreateSupportedAttribute(
                                    itemIdentifiers.ItemDefinitionIdentifier,
                                    new StringIdentifier("cloth-hood")),
                            },
                            new IGeneratorComponent[]
                            {
                                new NameGeneratorComponent("Cloth Hood"),
                                new IconGeneratorComponent(new StringIdentifier(@"graphics\items\helms\hood")),
                                new EquippableGeneratorComponent(new[] { new StringIdentifier("head") }),
                                new SocketGeneratorComponent(new[]
                                {
                                    KeyValuePair.Create((IIdentifier)new StringIdentifier("gem"), Tuple.Create(0, 4)),
                                }),
                                new HasStatsGeneratorComponent(new Dictionary<IIdentifier, double>()
                                {
                                    [new StringIdentifier("armor")] = 1, // FIXME: just for testing
                                }),
                            }),
                        new ItemDefinition(
                            new[]
                            {
                                AffixFilterAttributes.AllowsNormalMagicAndRareAffix,
                                filterContextAmenity.CreateSupportedAttribute(
                                    itemIdentifiers.ItemDefinitionIdentifier,
                                    new StringIdentifier("cloth-armor")),
                            },
                            new IGeneratorComponent[]
                            {
                                new NameGeneratorComponent("Cloth Armor"),
                                new IconGeneratorComponent(new StringIdentifier(@"graphics\items\body\cloth_armor")),
                                new EquippableGeneratorComponent(new[] { new StringIdentifier("body") }),
                                new SocketGeneratorComponent(new[]
                                {
                                    KeyValuePair.Create((IIdentifier)new StringIdentifier("gem"), Tuple.Create(0, 6)),
                                }),
                                new HasStatsGeneratorComponent(new Dictionary<IIdentifier, double>()
                                {
                                    [new StringIdentifier("armor")] = 3, // FIXME: just for testing
                                }),
                            }),
                        new ItemDefinition(
                            new[]
                            {
                                AffixFilterAttributes.RequiresNormalAffix,
                                filterContextAmenity.CreateSupportedAttribute(
                                    itemIdentifiers.ItemDefinitionIdentifier,
                                    new StringIdentifier("junk")),
                            },
                            new IGeneratorComponent[]
                            {
                                new NameGeneratorComponent("Junk"),
                                new IconGeneratorComponent(new StringIdentifier(@"graphics\items\junk")),
                            }),
                        new ItemDefinition(
                            new[]
                            {
                                AffixFilterAttributes.RequiresNormalAffix,
                                filterContextAmenity.CreateSupportedAttribute(
                                    itemIdentifiers.ItemDefinitionIdentifier,
                                    new StringIdentifier("ruby")),
                            },
                            new IGeneratorComponent[]
                            {
                                new NameGeneratorComponent("Ruby"),
                                new IconGeneratorComponent(new StringIdentifier(@"graphics\items\gems\ruby")),
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
