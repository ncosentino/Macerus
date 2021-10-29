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
    public sealed class BaseWeaponsModule : SingleRegistrationModule
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
                                    new StringIdentifier("weapon_1")),
                            },
                            new IGeneratorComponent[]
                            {
                                new BaseItemInventoryNameGeneratorComponent(new StringIdentifier("weapon_name_1")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                new EquippableGeneratorComponent(new[] { new StringIdentifier("Left Hand, Right Hand") }),
                                
                                new ItemTagsGeneratorComponent(new IIdentifier[]
                                {
                                    new StringIdentifier("weapon"),
                                    new StringIdentifier("melee"),
                                    new StringIdentifier("blade"),
                                    new StringIdentifier("1handed"),
                                    new StringIdentifier("dagger")
                                }),
                                new HasStatsGeneratorComponent(new Dictionary<IIdentifier, double>()
                                {
                                    [new StringIdentifier("stat_93")] = 0, // item level
                                    [new StringIdentifier("stat_64")] = 1, // attack speed
                                    [new StringIdentifier("stat_125")] = 0, // range
                                    // requirements
                                    // none!

                                    // durability
                                    [new StringIdentifier("stat_123")] = 0, // durability maximum
                                    [new StringIdentifier("stat_122")] = 0, // durability current
                                }),
                                new RandomStatRangeGeneratorComponent(new StringIdentifier("stat_19"), 1, 2, 0), // physical damage min
                                new RandomStatRangeGeneratorComponent(new StringIdentifier("stat_29"), 3, 4, 0), // physical damage max
                                
                            }),
						
                        new ItemDefinition(
                            new[]
                            {
                                AffixFilterAttributes.AllowsNormalMagicAndRareAffix,
                                filterContextAmenity.CreateSupportedAttribute(
                                    itemIdentifiers.ItemDefinitionIdentifier,
                                    new StringIdentifier("weapon_2")),
                            },
                            new IGeneratorComponent[]
                            {
                                new BaseItemInventoryNameGeneratorComponent(new StringIdentifier("weapon_name_2")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                new EquippableGeneratorComponent(new[] { new StringIdentifier("Left Hand, Right Hand") }),
                                
                                new ItemTagsGeneratorComponent(new IIdentifier[]
                                {
                                    new StringIdentifier("weapon"),
                                    new StringIdentifier("melee"),
                                    new StringIdentifier("blade"),
                                    new StringIdentifier("1handed"),
                                    new StringIdentifier("sword")
                                }),
                                new HasStatsGeneratorComponent(new Dictionary<IIdentifier, double>()
                                {
                                    [new StringIdentifier("stat_93")] = 0, // item level
                                    [new StringIdentifier("stat_64")] = 1, // attack speed
                                    [new StringIdentifier("stat_125")] = 0, // range
                                    // requirements
                                    // none!

                                    // durability
                                    [new StringIdentifier("stat_123")] = 0, // durability maximum
                                    [new StringIdentifier("stat_122")] = 0, // durability current
                                }),
                                new RandomStatRangeGeneratorComponent(new StringIdentifier("stat_19"), 2, 3, 0), // physical damage min
                                new RandomStatRangeGeneratorComponent(new StringIdentifier("stat_29"), 4, 5, 0), // physical damage max
                                
                            }),
						
                        new ItemDefinition(
                            new[]
                            {
                                AffixFilterAttributes.AllowsNormalMagicAndRareAffix,
                                filterContextAmenity.CreateSupportedAttribute(
                                    itemIdentifiers.ItemDefinitionIdentifier,
                                    new StringIdentifier("weapon_3")),
                            },
                            new IGeneratorComponent[]
                            {
                                new BaseItemInventoryNameGeneratorComponent(new StringIdentifier("weapon_name_3")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                new EquippableGeneratorComponent(new[] { new StringIdentifier("Left Hand, Right Hand") }),
                                
                                new ItemTagsGeneratorComponent(new IIdentifier[]
                                {
                                    new StringIdentifier("weapon"),
                                    new StringIdentifier("melee"),
                                    new StringIdentifier("blade"),
                                    new StringIdentifier("1handed"),
                                    new StringIdentifier("sword")
                                }),
                                new HasStatsGeneratorComponent(new Dictionary<IIdentifier, double>()
                                {
                                    [new StringIdentifier("stat_93")] = 0, // item level
                                    [new StringIdentifier("stat_64")] = 1, // attack speed
                                    [new StringIdentifier("stat_125")] = 1, // range
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
                                    new StringIdentifier("weapon_4")),
                            },
                            new IGeneratorComponent[]
                            {
                                new BaseItemInventoryNameGeneratorComponent(new StringIdentifier("weapon_name_4")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                new EquippableGeneratorComponent(new[] { new StringIdentifier("Left Hand, Right Hand") }),
                                
                                new ItemTagsGeneratorComponent(new IIdentifier[]
                                {
                                    new StringIdentifier("weapon"),
                                    new StringIdentifier("melee"),
                                    new StringIdentifier("blade"),
                                    new StringIdentifier("1handed"),
                                    new StringIdentifier("sword")
                                }),
                                new HasStatsGeneratorComponent(new Dictionary<IIdentifier, double>()
                                {
                                    [new StringIdentifier("stat_93")] = 0, // item level
                                    [new StringIdentifier("stat_64")] = 0, // attack speed
                                    [new StringIdentifier("stat_125")] = 1, // range
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
                                    new StringIdentifier("weapon_5")),
                            },
                            new IGeneratorComponent[]
                            {
                                new BaseItemInventoryNameGeneratorComponent(new StringIdentifier("weapon_name_5")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                new EquippableGeneratorComponent(new[] { new StringIdentifier("Left Hand, Right Hand") }),
                                
                                new ItemTagsGeneratorComponent(new IIdentifier[]
                                {
                                    new StringIdentifier("weapon"),
                                    new StringIdentifier("magic"),
                                    new StringIdentifier("1handed"),
                                    new StringIdentifier("wand")
                                }),
                                new HasStatsGeneratorComponent(new Dictionary<IIdentifier, double>()
                                {
                                    [new StringIdentifier("stat_93")] = 0, // item level
                                    [new StringIdentifier("stat_64")] = 1, // attack speed
                                    [new StringIdentifier("stat_125")] = 4, // range
                                    // requirements
                                    // none!

                                    // durability
                                    [new StringIdentifier("stat_123")] = 0, // durability maximum
                                    [new StringIdentifier("stat_122")] = 0, // durability current
                                }),
                                
                                new RandomStatRangeGeneratorComponent(new StringIdentifier("stat_20"), 2, 3, 0), // magic damage min
                                new RandomStatRangeGeneratorComponent(new StringIdentifier("stat_30"), 4, 5, 0), // magic damage max
                            }),
						
                        new ItemDefinition(
                            new[]
                            {
                                AffixFilterAttributes.AllowsNormalMagicAndRareAffix,
                                filterContextAmenity.CreateSupportedAttribute(
                                    itemIdentifiers.ItemDefinitionIdentifier,
                                    new StringIdentifier("weapon_6")),
                            },
                            new IGeneratorComponent[]
                            {
                                new BaseItemInventoryNameGeneratorComponent(new StringIdentifier("weapon_name_6")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                new EquippableGeneratorComponent(new[] { new StringIdentifier("2 Handed") }),
                                
                                new ItemTagsGeneratorComponent(new IIdentifier[]
                                {
                                    new StringIdentifier("weapon"),
                                    new StringIdentifier("magic"),
                                    new StringIdentifier("2handed"),
                                    new StringIdentifier("staff")
                                }),
                                new HasStatsGeneratorComponent(new Dictionary<IIdentifier, double>()
                                {
                                    [new StringIdentifier("stat_93")] = 0, // item level
                                    [new StringIdentifier("stat_64")] = 0, // attack speed
                                    [new StringIdentifier("stat_125")] = 5, // range
                                    // requirements
                                    // none!

                                    // durability
                                    [new StringIdentifier("stat_123")] = 0, // durability maximum
                                    [new StringIdentifier("stat_122")] = 0, // durability current
                                }),
                                
                                new RandomStatRangeGeneratorComponent(new StringIdentifier("stat_20"), 3, 4, 0), // magic damage min
                                new RandomStatRangeGeneratorComponent(new StringIdentifier("stat_30"), 7, 9, 0), // magic damage max
                            }),
						
                        new ItemDefinition(
                            new[]
                            {
                                AffixFilterAttributes.AllowsNormalMagicAndRareAffix,
                                filterContextAmenity.CreateSupportedAttribute(
                                    itemIdentifiers.ItemDefinitionIdentifier,
                                    new StringIdentifier("weapon_7")),
                            },
                            new IGeneratorComponent[]
                            {
                                new BaseItemInventoryNameGeneratorComponent(new StringIdentifier("weapon_name_7")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                new EquippableGeneratorComponent(new[] { new StringIdentifier("Left Hand, Right Hand") }),
                                
                                new ItemTagsGeneratorComponent(new IIdentifier[]
                                {
                                    new StringIdentifier("weapon"),
                                    new StringIdentifier("melee"),
                                    new StringIdentifier("blade"),
                                    new StringIdentifier("1handed"),
                                    new StringIdentifier("axe")
                                }),
                                new HasStatsGeneratorComponent(new Dictionary<IIdentifier, double>()
                                {
                                    [new StringIdentifier("stat_93")] = 0, // item level
                                    [new StringIdentifier("stat_64")] = 0, // attack speed
                                    [new StringIdentifier("stat_125")] = 1, // range
                                    // requirements
                                    // none!

                                    // durability
                                    [new StringIdentifier("stat_123")] = 0, // durability maximum
                                    [new StringIdentifier("stat_122")] = 0, // durability current
                                }),
                                new RandomStatRangeGeneratorComponent(new StringIdentifier("stat_19"), 3, 4, 0), // physical damage min
                                new RandomStatRangeGeneratorComponent(new StringIdentifier("stat_29"), 4, 5, 0), // physical damage max
                                
                            }),
						
                        new ItemDefinition(
                            new[]
                            {
                                AffixFilterAttributes.AllowsNormalMagicAndRareAffix,
                                filterContextAmenity.CreateSupportedAttribute(
                                    itemIdentifiers.ItemDefinitionIdentifier,
                                    new StringIdentifier("weapon_8")),
                            },
                            new IGeneratorComponent[]
                            {
                                new BaseItemInventoryNameGeneratorComponent(new StringIdentifier("weapon_name_8")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                new EquippableGeneratorComponent(new[] { new StringIdentifier("2 Handed") }),
                                
                                new ItemTagsGeneratorComponent(new IIdentifier[]
                                {
                                    new StringIdentifier("weapon"),
                                    new StringIdentifier("melee"),
                                    new StringIdentifier("blade"),
                                    new StringIdentifier("2handed"),
                                    new StringIdentifier("axe")
                                }),
                                new HasStatsGeneratorComponent(new Dictionary<IIdentifier, double>()
                                {
                                    [new StringIdentifier("stat_93")] = 0, // item level
                                    [new StringIdentifier("stat_64")] = 0, // attack speed
                                    [new StringIdentifier("stat_125")] = 1, // range
                                    // requirements
                                    // none!

                                    // durability
                                    [new StringIdentifier("stat_123")] = 0, // durability maximum
                                    [new StringIdentifier("stat_122")] = 0, // durability current
                                }),
                                new RandomStatRangeGeneratorComponent(new StringIdentifier("stat_19"), 3, 4, 0), // physical damage min
                                new RandomStatRangeGeneratorComponent(new StringIdentifier("stat_29"), 6, 9, 0), // physical damage max
                                
                            }),
						
                        new ItemDefinition(
                            new[]
                            {
                                AffixFilterAttributes.AllowsNormalMagicAndRareAffix,
                                filterContextAmenity.CreateSupportedAttribute(
                                    itemIdentifiers.ItemDefinitionIdentifier,
                                    new StringIdentifier("weapon_9")),
                            },
                            new IGeneratorComponent[]
                            {
                                new BaseItemInventoryNameGeneratorComponent(new StringIdentifier("weapon_name_9")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                new EquippableGeneratorComponent(new[] { new StringIdentifier("2 Handed") }),
                                
                                new ItemTagsGeneratorComponent(new IIdentifier[]
                                {
                                    new StringIdentifier("weapon"),
                                    new StringIdentifier("melee"),
                                    new StringIdentifier("2handed"),
                                    new StringIdentifier("spear")
                                }),
                                new HasStatsGeneratorComponent(new Dictionary<IIdentifier, double>()
                                {
                                    [new StringIdentifier("stat_93")] = 0, // item level
                                    [new StringIdentifier("stat_64")] = 0, // attack speed
                                    [new StringIdentifier("stat_125")] = 2, // range
                                    // requirements
                                    // none!

                                    // durability
                                    [new StringIdentifier("stat_123")] = 0, // durability maximum
                                    [new StringIdentifier("stat_122")] = 0, // durability current
                                }),
                                new RandomStatRangeGeneratorComponent(new StringIdentifier("stat_19"), 2, 4, 0), // physical damage min
                                new RandomStatRangeGeneratorComponent(new StringIdentifier("stat_29"), 7, 9, 0), // physical damage max
                                
                            }),
						
                        new ItemDefinition(
                            new[]
                            {
                                AffixFilterAttributes.AllowsNormalMagicAndRareAffix,
                                filterContextAmenity.CreateSupportedAttribute(
                                    itemIdentifiers.ItemDefinitionIdentifier,
                                    new StringIdentifier("weapon_10")),
                            },
                            new IGeneratorComponent[]
                            {
                                new BaseItemInventoryNameGeneratorComponent(new StringIdentifier("weapon_name_10")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                new EquippableGeneratorComponent(new[] { new StringIdentifier("2 Handed") }),
                                
                                new ItemTagsGeneratorComponent(new IIdentifier[]
                                {
                                    new StringIdentifier("weapon"),
                                    new StringIdentifier("ranged"),
                                    new StringIdentifier("2handed"),
                                    new StringIdentifier("bow")
                                }),
                                new HasStatsGeneratorComponent(new Dictionary<IIdentifier, double>()
                                {
                                    [new StringIdentifier("stat_93")] = 0, // item level
                                    [new StringIdentifier("stat_64")] = 1, // attack speed
                                    [new StringIdentifier("stat_125")] = 7, // range
                                    // requirements
                                    // none!

                                    // durability
                                    [new StringIdentifier("stat_123")] = 0, // durability maximum
                                    [new StringIdentifier("stat_122")] = 0, // durability current
                                }),
                                new RandomStatRangeGeneratorComponent(new StringIdentifier("stat_19"), 1, 4, 0), // physical damage min
                                new RandomStatRangeGeneratorComponent(new StringIdentifier("stat_29"), 4, 8, 0), // physical damage max
                                
                            }),
						
                        new ItemDefinition(
                            new[]
                            {
                                AffixFilterAttributes.AllowsNormalMagicAndRareAffix,
                                filterContextAmenity.CreateSupportedAttribute(
                                    itemIdentifiers.ItemDefinitionIdentifier,
                                    new StringIdentifier("weapon_11")),
                            },
                            new IGeneratorComponent[]
                            {
                                new BaseItemInventoryNameGeneratorComponent(new StringIdentifier("weapon_name_11")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                new EquippableGeneratorComponent(new[] { new StringIdentifier("2 Handed") }),
                                
                                new ItemTagsGeneratorComponent(new IIdentifier[]
                                {
                                    new StringIdentifier("weapon"),
                                    new StringIdentifier("ranged"),
                                    new StringIdentifier("2handed"),
                                    new StringIdentifier("bow")
                                }),
                                new HasStatsGeneratorComponent(new Dictionary<IIdentifier, double>()
                                {
                                    [new StringIdentifier("stat_93")] = 0, // item level
                                    [new StringIdentifier("stat_64")] = 0, // attack speed
                                    [new StringIdentifier("stat_125")] = 8, // range
                                    // requirements
                                    // none!

                                    // durability
                                    [new StringIdentifier("stat_123")] = 0, // durability maximum
                                    [new StringIdentifier("stat_122")] = 0, // durability current
                                }),
                                new RandomStatRangeGeneratorComponent(new StringIdentifier("stat_19"), 1, 4, 0), // physical damage min
                                new RandomStatRangeGeneratorComponent(new StringIdentifier("stat_29"), 6, 10, 0), // physical damage max
                                
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
