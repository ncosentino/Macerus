using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Macerus.ContentConverter
{
    public sealed class UniqueItemCodeWriter
    {
        public void WriteUniqueItemsCode(
            IEnumerable<UniqueItemDto> uniqueItemDtos,
            string outputDirectory)
        {
            var uniqueItemCode = @$"using System;
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
{{
    public sealed class UniqueItemModule : SingleRegistrationModule
    {{
        private static readonly Lazy<IFilterAttribute> REQUIRES_UNIQUE_AFFIX = new Lazy<IFilterAttribute>(() =>
            new FilterAttribute(
                new StringIdentifier(""affix-type""),
                new StringFilterAttributeValue(""unique""),
                true));

        private IFilterAttribute RequiresUniqueAffixAttribute => REQUIRES_UNIQUE_AFFIX.Value;

        protected override void SafeLoad(ContainerBuilder builder)
        {{
            builder
                .Register(c =>
                {{
                    var enchantmentIdentifiers = c.Resolve<IEnchantmentIdentifiers>();
                    var itemDefinitions = new[]
                    {{
{string.Join(",\r\n", uniqueItemDtos.Select(x => "                    " + GetUniqueItemCodeTemplateFromDto(x)))}
                    }};
                    var itemDefinitionRepository = new InMemoryItemDefinitionRepository(
                        c.Resolve<IAttributeFilterer>(),
                        itemDefinitions);
                    return itemDefinitionRepository;
                }})
                .SingleInstance()
                .AsImplementedInterfaces();
        }}

        private IFilterAttribute CreateItemIdFilterAttribute(IIdentifier identifier) =>
            new FilterAttribute(
                new StringIdentifier(""item-id""),
                new IdentifierFilterAttributeValue(identifier),
                false);
    }}
}}
";
            var directoryPath = Path.Combine(outputDirectory, @"Generated\Items");
            Directory.CreateDirectory(directoryPath);

            var filePath = Path.Combine(directoryPath, "UniqueItemModule.cs");
            File.Delete(filePath);
            File.WriteAllText(filePath, uniqueItemCode);
        }

        private string GetUniqueItemCodeTemplateFromDto(UniqueItemDto uniqueItemDto)
        {           
            var enchantmentComponentCode = uniqueItemDto.EnchantmentDefinitionIds.Count < 1
                    ? string.Empty
                    : @$"new EnchantmentsGeneratorComponent(new IFilterAttribute[][]
                                {{
{string.Join(",\r\n", uniqueItemDto.EnchantmentDefinitionIds.Select(enchantmentDefinitionId => @$"                                  new IFilterAttribute[]
                                   {{
                                        new FilterAttribute(
                                            enchantmentIdentifiers.EnchantmentDefinitionId,
                                            new IdentifierFilterAttributeValue(new StringIdentifier(""{enchantmentDefinitionId}"")),
                                            true),
                                   }}"))}
                                }}),";

            var uniqueItemCodeTemplate = @$"
                        new ItemDefinition(
                            new[]
                            {{
                                RequiresUniqueAffixAttribute,
                                CreateItemIdFilterAttribute(new StringIdentifier(""{uniqueItemDto.UniqueItemId}"")),
                            }},
                            new IGeneratorComponent[]
                            {{
                                new UniqueBaseItemGeneratorComponent(new StringIdentifier(""{uniqueItemDto.BaseItemId}"")),
                                new UniqueItemInventoryNameGeneratorComponent(new StringIdentifier(""{uniqueItemDto.ItemNameStringResourceId}"")),
                                new IconGeneratorComponent(new StringIdentifier(@""{uniqueItemDto.ItemIconResourceDto.RelativeResourcePath}"")),
                                {enchantmentComponentCode}
                            }})";
            return uniqueItemCodeTemplate;
        }
    }
}
