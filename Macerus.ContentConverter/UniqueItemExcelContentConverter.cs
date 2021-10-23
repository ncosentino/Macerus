using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

using ProjectXyz.Plugins.Features.Stats;

namespace Macerus.ContentConverter
{
    public sealed class UniqueItemExcelContentConverter : IUniqueItemExcelContentConverter
    {
        private static readonly Regex ENCHANTMENT_REGEX = new Regex(
            @"([-+\*])\s*\((\d*\.?\d+)\s*-\s*(\d*\.?\d+),\s*(\d+)\)\s*\((.+)\)",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private readonly ISheetHelper _sheetHelper;
        private readonly IStringResourceContentConverter _stringResourceContentConverter;

        public UniqueItemExcelContentConverter(
            ISheetHelper sheetHelper,
            IStringResourceContentConverter stringResourceContentConverter)
        {
            _sheetHelper = sheetHelper;
            _stringResourceContentConverter = stringResourceContentConverter;
        }

        public void ConvertUniqueItems(
            XSSFWorkbook workbook,
            IReadOnlyStatDefinitionToTermMappingRepository statDefinitionToTermMappingRepository)
        {
            var uniqueItemsSheet = workbook.GetSheet("Unique Items");
            var columnHeaderMapping = _sheetHelper.GetColumnHeaderMapping(uniqueItemsSheet.GetRow(0));

            var uniqueItemContent = GetUniqueItemContent(
                uniqueItemsSheet,
                columnHeaderMapping,
                statDefinitionToTermMappingRepository)
                .ToArray();

            var uniqueItemCode = @$"
using System;
using System.Collections.Generic; // needed for NET5

using Autofac;

using Macerus.Plugins.Features.GameObjects.Items;
using Macerus.Plugins.Features.GameObjects.Items.Behaviors;
using Macerus.Plugins.Features.GameObjects.Items.Socketing;

using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Filtering.Default.Attributes; // FIXME: dependency on non-API
using ProjectXyz.Plugins.Features.GameObjects.Generation.Default;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.InMemory;
using ProjectXyz.Shared.Framework;
using Macerus.Plugins.Features.GameObjects.Items.Generation.Unique;

namespace Macerus.Content.Items
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
                    var itemDefinitions = new[]
                    {{
{string.Join(",\r\n", uniqueItemContent.Select(x => "                    " + x.ItemCodeTemplate))}
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
            var directoryPath = @"Generated\Items\Unique";
            Directory.CreateDirectory(directoryPath);

            var filePath = Path.Combine(directoryPath, "UniqueItemModule.cs");
            File.Delete(filePath);
            File.WriteAllText(filePath, uniqueItemCode);

            _stringResourceContentConverter.WriteStringResourceModule(
                "Macerus.Content.Generated.Items",
                "UniqueItemStringResourcesModule",
                uniqueItemContent.Select(x => x.StringResourceKvp),
                Path.Combine(directoryPath, "UniqueItemStringResourcesModule.cs"));
        }

        private IEnumerable<UniqueItemContent> GetUniqueItemContent(
            ISheet uniqueItemsSheet,
            IReadOnlyDictionary<string, int> columnHeaderMapping,
            IReadOnlyStatDefinitionToTermMappingRepository statDefinitionToTermMappingRepository)
        {
            for (int rowIndex = 1; rowIndex < uniqueItemsSheet.PhysicalNumberOfRows; rowIndex++)
            {
                var row = uniqueItemsSheet.GetRow(rowIndex);

                var uniqueItemId = $"unique_item_{rowIndex}";
                var enchantmentIdsAndCodeTemplates = GetEnchantmentCodeTemplates(
                    row,
                    columnHeaderMapping,
                    statDefinitionToTermMappingRepository,
                    uniqueItemId + "_enchantment")
                    .ToArray();

                var baseItemId = row.GetCell(columnHeaderMapping["base item"]).StringCellValue;

                var itemNameStringResource = row.GetCell(columnHeaderMapping["name"]).StringCellValue;
                var itemNameStringResourceId = $"unique_item_name_{rowIndex}";

                var itemIconResource = row.GetCell(columnHeaderMapping["icon"]).StringCellValue;
                var itemIconResourceId = $"unique_item_icon_{rowIndex}";

                var enchantmentComponentCode = enchantmentIdsAndCodeTemplates.Length < 1
                    ? string.Empty
                    :
                    @$"new EnchantmentsGeneratorComponent(
                                    {enchantmentIdsAndCodeTemplates.Length},
                                    {enchantmentIdsAndCodeTemplates.Length},
                                    new[]
                                    {{
                                        new FilterAttribute(
                                            _enchantmentIdentifiers.EnchantmentDefinitionId,
                                            new AllIdentifierCollectionFilterAttributeValue(
{string.Join(",\r\n", enchantmentIdsAndCodeTemplates.Select(x => @$"                                                new StringIdentifier(""{x.Key}"")"))},
                                            true),
                                    }}),";

                var uniqueItemCodeTemplate = @$"
                        new ItemDefinition(
                            new[]
                            {{
                                RequiresUniqueAffixAttribute,
                                CreateItemIdFilterAttribute(new StringIdentifier(""{uniqueItemId}"")),
                            }},
                            new IGeneratorComponent[]
                            {{
                                new UniqueBaseItemGeneratorComponent(new StringIdentifier(""{baseItemId}"")),
                                new NameGeneratorComponent(""{itemNameStringResourceId}""),
                                new IconGeneratorComponent(""{itemIconResourceId}""),
                                {enchantmentComponentCode}
                            }})";

                yield return new UniqueItemContent(
                    uniqueItemCodeTemplate,
                    enchantmentIdsAndCodeTemplates.Select(x => x.Value).ToArray(),
                    new KeyValuePair<string, string>(itemNameStringResourceId, itemNameStringResource),
                    itemIconResourceId);// FIXME: get the code for this
            }
        }

        private IEnumerable<KeyValuePair<string, string>> GetEnchantmentCodeTemplates(
            IRow row,
            IReadOnlyDictionary<string, int> columnHeaderMapping,
            IReadOnlyStatDefinitionToTermMappingRepository statDefinitionToTermMappingRepository,
            string enchantmentIdentifierPrefix)
        {
            var counter = 0;
            for (int enchantmentCellIndex = 1; enchantmentCellIndex < 10; enchantmentCellIndex++)
            {
                var beforeCount = counter;

                var columnHeader = "Enchantment " + enchantmentCellIndex;
                var rawEnchantment = columnHeaderMapping.ContainsKey(columnHeader)
                    ? row.GetCell(columnHeaderMapping[columnHeader]).StringCellValue
                    : null;
                if (string.IsNullOrWhiteSpace(rawEnchantment))
                {
                    yield break;
                }

                foreach (var enchantmentIdAndCodeTemplate in GetEnchantmentIdsAndCodeTemplates(
                    rawEnchantment,
                    statDefinitionToTermMappingRepository,
                    enchantmentIdentifierPrefix,
                    counter))
                {
                    yield return enchantmentIdAndCodeTemplate;
                    counter++;
                }

                var afterCount = counter;
                if (afterCount <= beforeCount)
                {
                    break;
                }
            }
        }

        private IEnumerable<KeyValuePair<string, string>> GetEnchantmentIdsAndCodeTemplates(
            string rawEnchantment,
            IReadOnlyStatDefinitionToTermMappingRepository statDefinitionToTermMappingRepository,
            string enchantmentIdentifierPrefix,
            int startCount)
        {
            if (string.IsNullOrWhiteSpace(rawEnchantment))
            {
                yield break;
            }

            var match = ENCHANTMENT_REGEX.Match(rawEnchantment);
            if (!match.Success)
            {
                throw new FormatException(
                    $"Could not parse enchantment '{rawEnchantment}'.");
            }

            var modifier = match.Groups[1].Value;
            var rangeMin = double.Parse(match.Groups[2].Value, CultureInfo.InvariantCulture);
            var rangeMax = double.Parse(match.Groups[3].Value, CultureInfo.InvariantCulture);
            var decimalPlaces = int.Parse(match.Groups[4].Value, CultureInfo.InvariantCulture);
            var statTerms = match.Groups[5].Value.Replace(" ", string.Empty).Split(',', StringSplitOptions.RemoveEmptyEntries);

            int counter = 0;
            foreach (var statTerm in statTerms)
            {
                var enchantmentIdentifier = enchantmentIdentifierPrefix + "_" + (startCount + counter);
                var enchantmentCodeTemplate = @$"
                enchantmentTemplate.CreateRangeEnchantment(
                    new StringIdentifier(""{enchantmentIdentifier}""),
                    new StringIdentifier(""{statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm(statTerm).StatDefinitionId}""), // {statTerm}
                    {modifier},
                    {rangeMin},
                    {rangeMax},
                    {decimalPlaces})";
                yield return new KeyValuePair<string, string>(
                    enchantmentIdentifier,
                    enchantmentCodeTemplate);
                counter++;
            }
        }
    }
}
