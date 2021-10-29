using System;
using System.Collections.Generic; // needed for NET5

using Autofac;

using Macerus.Plugins.Features.GameObjects.Items;
using Macerus.Plugins.Features.GameObjects.Items.Generation.Unique;

using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.Filtering.Default.Attributes; // FIXME: dependency on non-API
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.InMemory;
using ProjectXyz.Shared.Framework;

namespace Macerus.Content.Generated.Items
{
    public sealed class UniqueItemModule : SingleRegistrationModule
    {
        private static readonly Lazy<IFilterAttribute> REQUIRES_UNIQUE_AFFIX = new Lazy<IFilterAttribute>(() =>
            new FilterAttribute(
                new StringIdentifier("affix-type"),
                new StringFilterAttributeValue("unique"),
                true));

        private IFilterAttribute RequiresUniqueAffixAttribute => REQUIRES_UNIQUE_AFFIX.Value;

        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(c =>
                {
                    var enchantmentIdentifiers = c.Resolve<IEnchantmentIdentifiers>();
                    var itemDefinitions = new[]
                    {
                    
                        new ItemDefinition(
                            new[]
                            {
                                RequiresUniqueAffixAttribute,
                                CreateItemIdFilterAttribute(new StringIdentifier("unique_item_1")),
                            },
                            new IGeneratorComponent[]
                            {
                                new UniqueBaseItemGeneratorComponent(new StringIdentifier("weapon_7")),
                                new UniqueItemInventoryNameGeneratorComponent(new StringIdentifier("unique_item_name_1")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                new EnchantmentsGeneratorComponent(new IFilterAttribute[][]
                                {
                                  new IFilterAttribute[]
                                   {
                                        new FilterAttribute(
                                            enchantmentIdentifiers.EnchantmentDefinitionId,
                                            new IdentifierFilterAttributeValue(new StringIdentifier("unique_item_1_enchantment_0")),
                                            true),
                                   },
                                  new IFilterAttribute[]
                                   {
                                        new FilterAttribute(
                                            enchantmentIdentifiers.EnchantmentDefinitionId,
                                            new IdentifierFilterAttributeValue(new StringIdentifier("unique_item_1_enchantment_1")),
                                            true),
                                   },
                                  new IFilterAttribute[]
                                   {
                                        new FilterAttribute(
                                            enchantmentIdentifiers.EnchantmentDefinitionId,
                                            new IdentifierFilterAttributeValue(new StringIdentifier("unique_item_1_enchantment_2")),
                                            true),
                                   },
                                  new IFilterAttribute[]
                                   {
                                        new FilterAttribute(
                                            enchantmentIdentifiers.EnchantmentDefinitionId,
                                            new IdentifierFilterAttributeValue(new StringIdentifier("unique_item_1_enchantment_3")),
                                            true),
                                   },
                                  new IFilterAttribute[]
                                   {
                                        new FilterAttribute(
                                            enchantmentIdentifiers.EnchantmentDefinitionId,
                                            new IdentifierFilterAttributeValue(new StringIdentifier("unique_item_1_enchantment_4")),
                                            true),
                                   },
                                  new IFilterAttribute[]
                                   {
                                        new FilterAttribute(
                                            enchantmentIdentifiers.EnchantmentDefinitionId,
                                            new IdentifierFilterAttributeValue(new StringIdentifier("unique_item_1_enchantment_5")),
                                            true),
                                   },
                                  new IFilterAttribute[]
                                   {
                                        new FilterAttribute(
                                            enchantmentIdentifiers.EnchantmentDefinitionId,
                                            new IdentifierFilterAttributeValue(new StringIdentifier("unique_item_1_enchantment_6")),
                                            true),
                                   },
                                  new IFilterAttribute[]
                                   {
                                        new FilterAttribute(
                                            enchantmentIdentifiers.EnchantmentDefinitionId,
                                            new IdentifierFilterAttributeValue(new StringIdentifier("unique_item_1_enchantment_7")),
                                            true),
                                   },
                                  new IFilterAttribute[]
                                   {
                                        new FilterAttribute(
                                            enchantmentIdentifiers.EnchantmentDefinitionId,
                                            new IdentifierFilterAttributeValue(new StringIdentifier("unique_item_1_enchantment_8")),
                                            true),
                                   },
                                  new IFilterAttribute[]
                                   {
                                        new FilterAttribute(
                                            enchantmentIdentifiers.EnchantmentDefinitionId,
                                            new IdentifierFilterAttributeValue(new StringIdentifier("unique_item_1_enchantment_9")),
                                            true),
                                   },
                                  new IFilterAttribute[]
                                   {
                                        new FilterAttribute(
                                            enchantmentIdentifiers.EnchantmentDefinitionId,
                                            new IdentifierFilterAttributeValue(new StringIdentifier("unique_item_1_enchantment_10")),
                                            true),
                                   }
                                }),
                            }),
                    
                        new ItemDefinition(
                            new[]
                            {
                                RequiresUniqueAffixAttribute,
                                CreateItemIdFilterAttribute(new StringIdentifier("unique_item_2")),
                            },
                            new IGeneratorComponent[]
                            {
                                new UniqueBaseItemGeneratorComponent(new StringIdentifier("armor_9")),
                                new UniqueItemInventoryNameGeneratorComponent(new StringIdentifier("unique_item_name_2")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                
                            }),
                    
                        new ItemDefinition(
                            new[]
                            {
                                RequiresUniqueAffixAttribute,
                                CreateItemIdFilterAttribute(new StringIdentifier("unique_item_3")),
                            },
                            new IGeneratorComponent[]
                            {
                                new UniqueBaseItemGeneratorComponent(new StringIdentifier("armor_1")),
                                new UniqueItemInventoryNameGeneratorComponent(new StringIdentifier("unique_item_name_3")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                
                            }),
                    
                        new ItemDefinition(
                            new[]
                            {
                                RequiresUniqueAffixAttribute,
                                CreateItemIdFilterAttribute(new StringIdentifier("unique_item_4")),
                            },
                            new IGeneratorComponent[]
                            {
                                new UniqueBaseItemGeneratorComponent(new StringIdentifier("weapon_1")),
                                new UniqueItemInventoryNameGeneratorComponent(new StringIdentifier("unique_item_name_4")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                
                            }),
                    
                        new ItemDefinition(
                            new[]
                            {
                                RequiresUniqueAffixAttribute,
                                CreateItemIdFilterAttribute(new StringIdentifier("unique_item_6")),
                            },
                            new IGeneratorComponent[]
                            {
                                new UniqueBaseItemGeneratorComponent(new StringIdentifier("weapon_1")),
                                new UniqueItemInventoryNameGeneratorComponent(new StringIdentifier("unique_item_name_6")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                
                            }),
                    
                        new ItemDefinition(
                            new[]
                            {
                                RequiresUniqueAffixAttribute,
                                CreateItemIdFilterAttribute(new StringIdentifier("unique_item_7")),
                            },
                            new IGeneratorComponent[]
                            {
                                new UniqueBaseItemGeneratorComponent(new StringIdentifier("armor_7")),
                                new UniqueItemInventoryNameGeneratorComponent(new StringIdentifier("unique_item_name_7")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                
                            }),
                    
                        new ItemDefinition(
                            new[]
                            {
                                RequiresUniqueAffixAttribute,
                                CreateItemIdFilterAttribute(new StringIdentifier("unique_item_8")),
                            },
                            new IGeneratorComponent[]
                            {
                                new UniqueBaseItemGeneratorComponent(new StringIdentifier("armor_8")),
                                new UniqueItemInventoryNameGeneratorComponent(new StringIdentifier("unique_item_name_8")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                
                            }),
                    
                        new ItemDefinition(
                            new[]
                            {
                                RequiresUniqueAffixAttribute,
                                CreateItemIdFilterAttribute(new StringIdentifier("unique_item_9")),
                            },
                            new IGeneratorComponent[]
                            {
                                new UniqueBaseItemGeneratorComponent(new StringIdentifier("weapon_5")),
                                new UniqueItemInventoryNameGeneratorComponent(new StringIdentifier("unique_item_name_9")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                
                            }),
                    
                        new ItemDefinition(
                            new[]
                            {
                                RequiresUniqueAffixAttribute,
                                CreateItemIdFilterAttribute(new StringIdentifier("unique_item_10")),
                            },
                            new IGeneratorComponent[]
                            {
                                new UniqueBaseItemGeneratorComponent(new StringIdentifier("armor_7")),
                                new UniqueItemInventoryNameGeneratorComponent(new StringIdentifier("unique_item_name_10")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                
                            }),
                    
                        new ItemDefinition(
                            new[]
                            {
                                RequiresUniqueAffixAttribute,
                                CreateItemIdFilterAttribute(new StringIdentifier("unique_item_11")),
                            },
                            new IGeneratorComponent[]
                            {
                                new UniqueBaseItemGeneratorComponent(new StringIdentifier("weapon_3")),
                                new UniqueItemInventoryNameGeneratorComponent(new StringIdentifier("unique_item_name_11")),
                                new IconGeneratorComponent(new StringIdentifier(@"Resources\Graphics\Items\Swords\red-gold-long.png")),
                                
                            }),
                    
                        new ItemDefinition(
                            new[]
                            {
                                RequiresUniqueAffixAttribute,
                                CreateItemIdFilterAttribute(new StringIdentifier("unique_item_12")),
                            },
                            new IGeneratorComponent[]
                            {
                                new UniqueBaseItemGeneratorComponent(new StringIdentifier("armor_7")),
                                new UniqueItemInventoryNameGeneratorComponent(new StringIdentifier("unique_item_name_12")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                
                            }),
                    
                        new ItemDefinition(
                            new[]
                            {
                                RequiresUniqueAffixAttribute,
                                CreateItemIdFilterAttribute(new StringIdentifier("unique_item_15")),
                            },
                            new IGeneratorComponent[]
                            {
                                new UniqueBaseItemGeneratorComponent(new StringIdentifier("weapon_2")),
                                new UniqueItemInventoryNameGeneratorComponent(new StringIdentifier("unique_item_name_15")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                
                            }),
                    
                        new ItemDefinition(
                            new[]
                            {
                                RequiresUniqueAffixAttribute,
                                CreateItemIdFilterAttribute(new StringIdentifier("unique_item_17")),
                            },
                            new IGeneratorComponent[]
                            {
                                new UniqueBaseItemGeneratorComponent(new StringIdentifier("weapon_5")),
                                new UniqueItemInventoryNameGeneratorComponent(new StringIdentifier("unique_item_name_17")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                new EnchantmentsGeneratorComponent(new IFilterAttribute[][]
                                {
                                  new IFilterAttribute[]
                                   {
                                        new FilterAttribute(
                                            enchantmentIdentifiers.EnchantmentDefinitionId,
                                            new IdentifierFilterAttributeValue(new StringIdentifier("unique_item_17_enchantment_0")),
                                            true),
                                   },
                                  new IFilterAttribute[]
                                   {
                                        new FilterAttribute(
                                            enchantmentIdentifiers.EnchantmentDefinitionId,
                                            new IdentifierFilterAttributeValue(new StringIdentifier("unique_item_17_enchantment_1")),
                                            true),
                                   },
                                  new IFilterAttribute[]
                                   {
                                        new FilterAttribute(
                                            enchantmentIdentifiers.EnchantmentDefinitionId,
                                            new IdentifierFilterAttributeValue(new StringIdentifier("unique_item_17_enchantment_2")),
                                            true),
                                   },
                                  new IFilterAttribute[]
                                   {
                                        new FilterAttribute(
                                            enchantmentIdentifiers.EnchantmentDefinitionId,
                                            new IdentifierFilterAttributeValue(new StringIdentifier("unique_item_17_enchantment_3")),
                                            true),
                                   },
                                  new IFilterAttribute[]
                                   {
                                        new FilterAttribute(
                                            enchantmentIdentifiers.EnchantmentDefinitionId,
                                            new IdentifierFilterAttributeValue(new StringIdentifier("unique_item_17_enchantment_4")),
                                            true),
                                   },
                                  new IFilterAttribute[]
                                   {
                                        new FilterAttribute(
                                            enchantmentIdentifiers.EnchantmentDefinitionId,
                                            new IdentifierFilterAttributeValue(new StringIdentifier("unique_item_17_enchantment_5")),
                                            true),
                                   }
                                }),
                            }),
                    
                        new ItemDefinition(
                            new[]
                            {
                                RequiresUniqueAffixAttribute,
                                CreateItemIdFilterAttribute(new StringIdentifier("unique_item_18")),
                            },
                            new IGeneratorComponent[]
                            {
                                new UniqueBaseItemGeneratorComponent(new StringIdentifier("armor_9")),
                                new UniqueItemInventoryNameGeneratorComponent(new StringIdentifier("unique_item_name_18")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                
                            }),
                    
                        new ItemDefinition(
                            new[]
                            {
                                RequiresUniqueAffixAttribute,
                                CreateItemIdFilterAttribute(new StringIdentifier("unique_item_19")),
                            },
                            new IGeneratorComponent[]
                            {
                                new UniqueBaseItemGeneratorComponent(new StringIdentifier("armor_2")),
                                new UniqueItemInventoryNameGeneratorComponent(new StringIdentifier("unique_item_name_19")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                
                            }),
                    
                        new ItemDefinition(
                            new[]
                            {
                                RequiresUniqueAffixAttribute,
                                CreateItemIdFilterAttribute(new StringIdentifier("unique_item_20")),
                            },
                            new IGeneratorComponent[]
                            {
                                new UniqueBaseItemGeneratorComponent(new StringIdentifier("weapon_6")),
                                new UniqueItemInventoryNameGeneratorComponent(new StringIdentifier("unique_item_name_20")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                
                            }),
                    
                        new ItemDefinition(
                            new[]
                            {
                                RequiresUniqueAffixAttribute,
                                CreateItemIdFilterAttribute(new StringIdentifier("unique_item_21")),
                            },
                            new IGeneratorComponent[]
                            {
                                new UniqueBaseItemGeneratorComponent(new StringIdentifier("weapon_9")),
                                new UniqueItemInventoryNameGeneratorComponent(new StringIdentifier("unique_item_name_21")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                
                            }),
                    
                        new ItemDefinition(
                            new[]
                            {
                                RequiresUniqueAffixAttribute,
                                CreateItemIdFilterAttribute(new StringIdentifier("unique_item_22")),
                            },
                            new IGeneratorComponent[]
                            {
                                new UniqueBaseItemGeneratorComponent(new StringIdentifier("weapon_10")),
                                new UniqueItemInventoryNameGeneratorComponent(new StringIdentifier("unique_item_name_22")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                
                            }),
                    
                        new ItemDefinition(
                            new[]
                            {
                                RequiresUniqueAffixAttribute,
                                CreateItemIdFilterAttribute(new StringIdentifier("unique_item_23")),
                            },
                            new IGeneratorComponent[]
                            {
                                new UniqueBaseItemGeneratorComponent(new StringIdentifier("weapon_11")),
                                new UniqueItemInventoryNameGeneratorComponent(new StringIdentifier("unique_item_name_23")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                
                            }),
                    
                        new ItemDefinition(
                            new[]
                            {
                                RequiresUniqueAffixAttribute,
                                CreateItemIdFilterAttribute(new StringIdentifier("unique_item_24")),
                            },
                            new IGeneratorComponent[]
                            {
                                new UniqueBaseItemGeneratorComponent(new StringIdentifier("armor_6")),
                                new UniqueItemInventoryNameGeneratorComponent(new StringIdentifier("unique_item_name_24")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                
                            }),
                    
                        new ItemDefinition(
                            new[]
                            {
                                RequiresUniqueAffixAttribute,
                                CreateItemIdFilterAttribute(new StringIdentifier("unique_item_25")),
                            },
                            new IGeneratorComponent[]
                            {
                                new UniqueBaseItemGeneratorComponent(new StringIdentifier("weapon_10")),
                                new UniqueItemInventoryNameGeneratorComponent(new StringIdentifier("unique_item_name_25")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                
                            }),
                    
                        new ItemDefinition(
                            new[]
                            {
                                RequiresUniqueAffixAttribute,
                                CreateItemIdFilterAttribute(new StringIdentifier("unique_item_26")),
                            },
                            new IGeneratorComponent[]
                            {
                                new UniqueBaseItemGeneratorComponent(new StringIdentifier("weapon_7")),
                                new UniqueItemInventoryNameGeneratorComponent(new StringIdentifier("unique_item_name_26")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                
                            }),
                    
                        new ItemDefinition(
                            new[]
                            {
                                RequiresUniqueAffixAttribute,
                                CreateItemIdFilterAttribute(new StringIdentifier("unique_item_27")),
                            },
                            new IGeneratorComponent[]
                            {
                                new UniqueBaseItemGeneratorComponent(new StringIdentifier("weapon_7")),
                                new UniqueItemInventoryNameGeneratorComponent(new StringIdentifier("unique_item_name_27")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                
                            }),
                    
                        new ItemDefinition(
                            new[]
                            {
                                RequiresUniqueAffixAttribute,
                                CreateItemIdFilterAttribute(new StringIdentifier("unique_item_28")),
                            },
                            new IGeneratorComponent[]
                            {
                                new UniqueBaseItemGeneratorComponent(new StringIdentifier("armor_4")),
                                new UniqueItemInventoryNameGeneratorComponent(new StringIdentifier("unique_item_name_28")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                
                            }),
                    
                        new ItemDefinition(
                            new[]
                            {
                                RequiresUniqueAffixAttribute,
                                CreateItemIdFilterAttribute(new StringIdentifier("unique_item_30")),
                            },
                            new IGeneratorComponent[]
                            {
                                new UniqueBaseItemGeneratorComponent(new StringIdentifier("armor_7")),
                                new UniqueItemInventoryNameGeneratorComponent(new StringIdentifier("unique_item_name_30")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                
                            }),
                    
                        new ItemDefinition(
                            new[]
                            {
                                RequiresUniqueAffixAttribute,
                                CreateItemIdFilterAttribute(new StringIdentifier("unique_item_31")),
                            },
                            new IGeneratorComponent[]
                            {
                                new UniqueBaseItemGeneratorComponent(new StringIdentifier("weapon_7")),
                                new UniqueItemInventoryNameGeneratorComponent(new StringIdentifier("unique_item_name_31")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                
                            }),
                    
                        new ItemDefinition(
                            new[]
                            {
                                RequiresUniqueAffixAttribute,
                                CreateItemIdFilterAttribute(new StringIdentifier("unique_item_32")),
                            },
                            new IGeneratorComponent[]
                            {
                                new UniqueBaseItemGeneratorComponent(new StringIdentifier("armor_3")),
                                new UniqueItemInventoryNameGeneratorComponent(new StringIdentifier("unique_item_name_32")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                
                            }),
                    
                        new ItemDefinition(
                            new[]
                            {
                                RequiresUniqueAffixAttribute,
                                CreateItemIdFilterAttribute(new StringIdentifier("unique_item_33")),
                            },
                            new IGeneratorComponent[]
                            {
                                new UniqueBaseItemGeneratorComponent(new StringIdentifier("armor_3")),
                                new UniqueItemInventoryNameGeneratorComponent(new StringIdentifier("unique_item_name_33")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                
                            }),
                    
                        new ItemDefinition(
                            new[]
                            {
                                RequiresUniqueAffixAttribute,
                                CreateItemIdFilterAttribute(new StringIdentifier("unique_item_34")),
                            },
                            new IGeneratorComponent[]
                            {
                                new UniqueBaseItemGeneratorComponent(new StringIdentifier("weapon_1")),
                                new UniqueItemInventoryNameGeneratorComponent(new StringIdentifier("unique_item_name_34")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                
                            }),
                    
                        new ItemDefinition(
                            new[]
                            {
                                RequiresUniqueAffixAttribute,
                                CreateItemIdFilterAttribute(new StringIdentifier("unique_item_36")),
                            },
                            new IGeneratorComponent[]
                            {
                                new UniqueBaseItemGeneratorComponent(new StringIdentifier("weapon_6")),
                                new UniqueItemInventoryNameGeneratorComponent(new StringIdentifier("unique_item_name_36")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                
                            }),
                    
                        new ItemDefinition(
                            new[]
                            {
                                RequiresUniqueAffixAttribute,
                                CreateItemIdFilterAttribute(new StringIdentifier("unique_item_37")),
                            },
                            new IGeneratorComponent[]
                            {
                                new UniqueBaseItemGeneratorComponent(new StringIdentifier("weapon_10")),
                                new UniqueItemInventoryNameGeneratorComponent(new StringIdentifier("unique_item_name_37")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                
                            }),
                    
                        new ItemDefinition(
                            new[]
                            {
                                RequiresUniqueAffixAttribute,
                                CreateItemIdFilterAttribute(new StringIdentifier("unique_item_38")),
                            },
                            new IGeneratorComponent[]
                            {
                                new UniqueBaseItemGeneratorComponent(new StringIdentifier("armor_8")),
                                new UniqueItemInventoryNameGeneratorComponent(new StringIdentifier("unique_item_name_38")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                
                            }),
                    
                        new ItemDefinition(
                            new[]
                            {
                                RequiresUniqueAffixAttribute,
                                CreateItemIdFilterAttribute(new StringIdentifier("unique_item_40")),
                            },
                            new IGeneratorComponent[]
                            {
                                new UniqueBaseItemGeneratorComponent(new StringIdentifier("armor_1")),
                                new UniqueItemInventoryNameGeneratorComponent(new StringIdentifier("unique_item_name_40")),
                                new IconGeneratorComponent(new StringIdentifier(@"TODO")),
                                
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

        private IFilterAttribute CreateItemIdFilterAttribute(IIdentifier identifier) =>
            new FilterAttribute(
                new StringIdentifier("item-id"),
                new IdentifierFilterAttributeValue(identifier),
                false);
    }
}
