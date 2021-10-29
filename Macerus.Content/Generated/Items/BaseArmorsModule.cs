using System;
using System.Collections.Generic;

using Autofac;

using Macerus.Api.Behaviors.Filtering;
using Macerus.Content.Affixes;
using Macerus.Plugins.Features.GameObjects.Items;
using Macerus.Plugins.Features.Stats.Default;
using Macerus.Shared.Behaviors;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Items;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.InMemory;
using ProjectXyz.Shared.Framework;

namespace Macerus.Content.Generated.Items
{
    public sealed class BaseArmorsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(c =>
                {
                    var filterContextAmenity = c.Resolve<IFilterContextAmenity>();
                    var itemIdentifiers = c.Resolve<IItemIdentifiers>();
                    var itemDefinitions = new[]
                    {
						
                        new ItemDefinition(
                            new[]
                            {
                                AffixFilterAttributes.AllowsNormalMagicAndRareAffix,
                                filterContextAmenity.CreateSupportedAttribute(
                                    itemIdentifiers.ItemDefinitionIdentifier,
                                    new StringIdentifier("armor_1")),
                            },
                            new IGeneratorComponent[]
                            {
                                new BaseItemInventoryNameGeneratorComponent(new StringIdentifier("armor_name_1")),
                                new IconGeneratorComponent(new StringIdentifier(@"Resources\Graphics\Items\Body\cloth_armor.png")),
                                new EquippableGeneratorComponent(new[] { new StringIdentifier("Body") }),
                                
                                new ItemTagsGeneratorComponent(new IIdentifier[]
                                {
                                    new StringIdentifier("armor"),
                                    new StringIdentifier("body")
                                }),
                                new HasStatsGeneratorComponent(new Dictionary<IIdentifier, double>()
                                {
                                    [new StringIdentifier("stat_93")] = 0, // item level
                                    [new StringIdentifier("stat_70")] = 0, // block
                                    // requirements
                                    // none!

                                    // durability
                                    [new StringIdentifier("stat_123")] = 0, // durability maximum
                                    [new StringIdentifier("stat_122")] = 0, // durability current
                                }),
                                
                                
                            }),
						
                        new ItemDefinition(
                            new[]
                            {
                                AffixFilterAttributes.AllowsNormalMagicAndRareAffix,
                                filterContextAmenity.CreateSupportedAttribute(
                                    itemIdentifiers.ItemDefinitionIdentifier,
                                    new StringIdentifier("armor_2")),
                            },
                            new IGeneratorComponent[]
                            {
                                new BaseItemInventoryNameGeneratorComponent(new StringIdentifier("armor_name_2")),
                                new IconGeneratorComponent(new StringIdentifier(@"Resources\Graphics\Items\Body\hood.png")),
                                new EquippableGeneratorComponent(new[] { new StringIdentifier("Head") }),
                                
                                new ItemTagsGeneratorComponent(new IIdentifier[]
                                {
                                    new StringIdentifier("armor"),
                                    new StringIdentifier("head")
                                }),
                                new HasStatsGeneratorComponent(new Dictionary<IIdentifier, double>()
                                {
                                    [new StringIdentifier("stat_93")] = 0, // item level
                                    [new StringIdentifier("stat_70")] = 0, // block
                                    // requirements
                                    // none!

                                    // durability
                                    [new StringIdentifier("stat_123")] = 0, // durability maximum
                                    [new StringIdentifier("stat_122")] = 0, // durability current
                                }),
                                
                                
                            }),
						
                        new ItemDefinition(
                            new[]
                            {
                                AffixFilterAttributes.AllowsNormalMagicAndRareAffix,
                                filterContextAmenity.CreateSupportedAttribute(
                                    itemIdentifiers.ItemDefinitionIdentifier,
                                    new StringIdentifier("armor_3")),
                            },
                            new IGeneratorComponent[]
                            {
                                new BaseItemInventoryNameGeneratorComponent(new StringIdentifier("armor_name_3")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                new EquippableGeneratorComponent(new[] { new StringIdentifier("Belt") }),
                                
                                new ItemTagsGeneratorComponent(new IIdentifier[]
                                {
                                    new StringIdentifier("armor"),
                                    new StringIdentifier("belt")
                                }),
                                new HasStatsGeneratorComponent(new Dictionary<IIdentifier, double>()
                                {
                                    [new StringIdentifier("stat_93")] = 0, // item level
                                    [new StringIdentifier("stat_70")] = 0, // block
                                    // requirements
                                    // none!

                                    // durability
                                    [new StringIdentifier("stat_123")] = 0, // durability maximum
                                    [new StringIdentifier("stat_122")] = 0, // durability current
                                }),
                                
                                
                            }),
						
                        new ItemDefinition(
                            new[]
                            {
                                AffixFilterAttributes.AllowsNormalMagicAndRareAffix,
                                filterContextAmenity.CreateSupportedAttribute(
                                    itemIdentifiers.ItemDefinitionIdentifier,
                                    new StringIdentifier("armor_4")),
                            },
                            new IGeneratorComponent[]
                            {
                                new BaseItemInventoryNameGeneratorComponent(new StringIdentifier("armor_name_4")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                new EquippableGeneratorComponent(new[] { new StringIdentifier("Feet") }),
                                
                                new ItemTagsGeneratorComponent(new IIdentifier[]
                                {
                                    new StringIdentifier("armor"),
                                    new StringIdentifier("feet")
                                }),
                                new HasStatsGeneratorComponent(new Dictionary<IIdentifier, double>()
                                {
                                    [new StringIdentifier("stat_93")] = 0, // item level
                                    [new StringIdentifier("stat_70")] = 0, // block
                                    // requirements
                                    // none!

                                    // durability
                                    [new StringIdentifier("stat_123")] = 0, // durability maximum
                                    [new StringIdentifier("stat_122")] = 0, // durability current
                                }),
                                
                                
                            }),
						
                        new ItemDefinition(
                            new[]
                            {
                                AffixFilterAttributes.AllowsNormalMagicAndRareAffix,
                                filterContextAmenity.CreateSupportedAttribute(
                                    itemIdentifiers.ItemDefinitionIdentifier,
                                    new StringIdentifier("armor_5")),
                            },
                            new IGeneratorComponent[]
                            {
                                new BaseItemInventoryNameGeneratorComponent(new StringIdentifier("armor_name_5")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                new EquippableGeneratorComponent(new[] { new StringIdentifier("Legs") }),
                                
                                new ItemTagsGeneratorComponent(new IIdentifier[]
                                {
                                    new StringIdentifier("armor"),
                                    new StringIdentifier("legs")
                                }),
                                new HasStatsGeneratorComponent(new Dictionary<IIdentifier, double>()
                                {
                                    [new StringIdentifier("stat_93")] = 0, // item level
                                    [new StringIdentifier("stat_70")] = 0, // block
                                    // requirements
                                    // none!

                                    // durability
                                    [new StringIdentifier("stat_123")] = 0, // durability maximum
                                    [new StringIdentifier("stat_122")] = 0, // durability current
                                }),
                                
                                
                            }),
						
                        new ItemDefinition(
                            new[]
                            {
                                AffixFilterAttributes.AllowsNormalMagicAndRareAffix,
                                filterContextAmenity.CreateSupportedAttribute(
                                    itemIdentifiers.ItemDefinitionIdentifier,
                                    new StringIdentifier("armor_6")),
                            },
                            new IGeneratorComponent[]
                            {
                                new BaseItemInventoryNameGeneratorComponent(new StringIdentifier("armor_name_6")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                new EquippableGeneratorComponent(new[] { new StringIdentifier("Back") }),
                                
                                new ItemTagsGeneratorComponent(new IIdentifier[]
                                {
                                    new StringIdentifier("armor"),
                                    new StringIdentifier("back")
                                }),
                                new HasStatsGeneratorComponent(new Dictionary<IIdentifier, double>()
                                {
                                    [new StringIdentifier("stat_93")] = 0, // item level
                                    [new StringIdentifier("stat_70")] = 0, // block
                                    // requirements
                                    // none!

                                    // durability
                                    [new StringIdentifier("stat_123")] = 0, // durability maximum
                                    [new StringIdentifier("stat_122")] = 0, // durability current
                                }),
                                
                                
                            }),
						
                        new ItemDefinition(
                            new[]
                            {
                                AffixFilterAttributes.AllowsNormalMagicAndRareAffix,
                                filterContextAmenity.CreateSupportedAttribute(
                                    itemIdentifiers.ItemDefinitionIdentifier,
                                    new StringIdentifier("armor_7")),
                            },
                            new IGeneratorComponent[]
                            {
                                new BaseItemInventoryNameGeneratorComponent(new StringIdentifier("armor_name_7")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                new EquippableGeneratorComponent(new[] { new StringIdentifier("Hands") }),
                                
                                new ItemTagsGeneratorComponent(new IIdentifier[]
                                {
                                    new StringIdentifier("armor"),
                                    new StringIdentifier("hands")
                                }),
                                new HasStatsGeneratorComponent(new Dictionary<IIdentifier, double>()
                                {
                                    [new StringIdentifier("stat_93")] = 0, // item level
                                    [new StringIdentifier("stat_70")] = 0, // block
                                    // requirements
                                    // none!

                                    // durability
                                    [new StringIdentifier("stat_123")] = 0, // durability maximum
                                    [new StringIdentifier("stat_122")] = 0, // durability current
                                }),
                                
                                
                            }),
						
                        new ItemDefinition(
                            new[]
                            {
                                AffixFilterAttributes.AllowsNormalMagicAndRareAffix,
                                filterContextAmenity.CreateSupportedAttribute(
                                    itemIdentifiers.ItemDefinitionIdentifier,
                                    new StringIdentifier("armor_8")),
                            },
                            new IGeneratorComponent[]
                            {
                                new BaseItemInventoryNameGeneratorComponent(new StringIdentifier("armor_name_8")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                new EquippableGeneratorComponent(new[] { new StringIdentifier("Ring1,Ring2") }),
                                
                                new ItemTagsGeneratorComponent(new IIdentifier[]
                                {
                                    new StringIdentifier("jewellery"),
                                    new StringIdentifier("ring")
                                }),
                                new HasStatsGeneratorComponent(new Dictionary<IIdentifier, double>()
                                {
                                    [new StringIdentifier("stat_93")] = 0, // item level
                                    [new StringIdentifier("stat_70")] = 0, // block
                                    // requirements
                                    // none!

                                    // durability
                                    [new StringIdentifier("stat_123")] = 0, // durability maximum
                                    [new StringIdentifier("stat_122")] = 0, // durability current
                                }),
                                
                                
                            }),
						
                        new ItemDefinition(
                            new[]
                            {
                                AffixFilterAttributes.AllowsNormalMagicAndRareAffix,
                                filterContextAmenity.CreateSupportedAttribute(
                                    itemIdentifiers.ItemDefinitionIdentifier,
                                    new StringIdentifier("armor_9")),
                            },
                            new IGeneratorComponent[]
                            {
                                new BaseItemInventoryNameGeneratorComponent(new StringIdentifier("armor_name_9")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                new EquippableGeneratorComponent(new[] { new StringIdentifier("Amulet") }),
                                
                                new ItemTagsGeneratorComponent(new IIdentifier[]
                                {
                                    new StringIdentifier("jewellery"),
                                    new StringIdentifier("amulet")
                                }),
                                new HasStatsGeneratorComponent(new Dictionary<IIdentifier, double>()
                                {
                                    [new StringIdentifier("stat_93")] = 0, // item level
                                    [new StringIdentifier("stat_70")] = 0, // block
                                    // requirements
                                    // none!

                                    // durability
                                    [new StringIdentifier("stat_123")] = 0, // durability maximum
                                    [new StringIdentifier("stat_122")] = 0, // durability current
                                }),
                                
                                
                            })
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
