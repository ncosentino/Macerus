﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

using ProjectXyz.Plugins.Features.Stats;

namespace Macerus.ContentConverter
{
    public sealed class UniqueItemExcelContentConverter
    {
        private static readonly Regex ENCHANTMENT_REGEX = new Regex(
            @"([-+\*])\s*\((\d*\.?\d+)\s*-\s*(\d*\.?\d+),\s*(\d+)\)\s*\((.+)\)",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private static readonly Regex FORCED_SOCKETS_REGEX = new Regex(
            @"FORCED_SOCKETS\(\d+,(\s*\w+,\s*\d+)(,\s*(\s*\w+,\s*\d+))*\)",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private readonly ISheetHelper _sheetHelper;

        public UniqueItemExcelContentConverter(ISheetHelper sheetHelper)
        {
            _sheetHelper = sheetHelper;
        }

        public IEnumerable<UniqueItemConvertedContent> GetUniqueItemContent(
            XSSFWorkbook workbook,
            IReadOnlyStatDefinitionToTermMappingRepository statDefinitionToTermMappingRepository)
        {
            var uniqueItemsSheet = workbook.GetSheet("Unique Items");
            var columnHeaderMapping = _sheetHelper.GetColumnHeaderMapping(uniqueItemsSheet.GetRow(0));
            return GetUniqueItemContent(
                uniqueItemsSheet,
                columnHeaderMapping,
                statDefinitionToTermMappingRepository);
        }

        private IEnumerable<UniqueItemConvertedContent> GetUniqueItemContent(
            ISheet uniqueItemsSheet,
            IReadOnlyDictionary<string, int> columnHeaderMapping,
            IReadOnlyStatDefinitionToTermMappingRepository statDefinitionToTermMappingRepository)
        {
            for (int rowIndex = 1; rowIndex < uniqueItemsSheet.PhysicalNumberOfRows; rowIndex++)
            {
                var row = uniqueItemsSheet.GetRow(rowIndex);

                var uniqueItemId = $"unique_item_{rowIndex}";
                var enchantmentDefinitionDtos = GetEnchantmentDefinitionDtos(
                    row,
                    columnHeaderMapping,
                    statDefinitionToTermMappingRepository,
                    uniqueItemId + "_enchantment")
                    .ToArray();

                var baseItemId = row.GetCell(columnHeaderMapping["base item"]).StringCellValue;
                if (row.GetCell(columnHeaderMapping["base item name"]).CachedFormulaResultType == CellType.Error)
                {
                    Console.WriteLine(
                        $"ERROR: Unique item {uniqueItemId} does not have a " +
                        $"valid base item mapped for ID '{baseItemId}'. Skipping...");
                    continue;
                }

                var itemNameStringResource = row.GetCell(columnHeaderMapping["name"]).StringCellValue;
                var itemNameStringResourceId = $"unique_item_name_{rowIndex}";
                var itemNameStringResourceDto = new StringResourceDto(
                    itemNameStringResourceId,
                    itemNameStringResource);

                var itemIconResourcePath = row.GetCell(columnHeaderMapping["icon"]).StringCellValue;
                var itemIconImageResourceDto = new ImageResourceDto(itemIconResourcePath);

                var uniqueItemDto = new UniqueItemDto(
                    uniqueItemId,
                    baseItemId,
                    itemNameStringResourceId,
                    itemIconImageResourceDto,
                    enchantmentDefinitionDtos.Select(x => x.EnchantmentDefinitionId).ToArray());
                yield return new UniqueItemConvertedContent(
                    uniqueItemDto,
                    enchantmentDefinitionDtos,
                    itemNameStringResourceDto,
                    itemIconImageResourceDto);
            }
        }

        private IEnumerable<EnchantmentDefinitionDto> GetEnchantmentDefinitionDtos(
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
                    ? row.GetCell(columnHeaderMapping[columnHeader])?.StringCellValue
                    : null;
                if (string.IsNullOrWhiteSpace(rawEnchantment))
                {
                    yield break;
                }

                foreach (var enchantmentIdAndCodeTemplate in GetEnchantmentDefinitionDtos(
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

        private IEnumerable<EnchantmentDefinitionDto> GetEnchantmentDefinitionDtos(
            string rawEnchantment,
            IReadOnlyStatDefinitionToTermMappingRepository statDefinitionToTermMappingRepository,
            string enchantmentIdentifierPrefix,
            int startCount)
        {
            if (string.IsNullOrWhiteSpace(rawEnchantment))
            {
                yield break;
            }

            var forcedSocketsMatch = FORCED_SOCKETS_REGEX.Match(rawEnchantment);
            if (forcedSocketsMatch.Success)
            {
                Console.WriteLine(
                    $"// TODO: no handling yet for forced sockets!\r\n\t{rawEnchantment}");
                yield break;
            }

            var enchantmentMatch = ENCHANTMENT_REGEX.Match(rawEnchantment);
            if (!enchantmentMatch.Success)
            {
                throw new FormatException(
                    $"Could not parse enchantment '{rawEnchantment}'.");
            }

            var modifier = enchantmentMatch.Groups[1].Value;
            var rangeMin = double.Parse(enchantmentMatch.Groups[2].Value, CultureInfo.InvariantCulture);
            var rangeMax = double.Parse(enchantmentMatch.Groups[3].Value, CultureInfo.InvariantCulture);
            var decimalPlaces = int.Parse(enchantmentMatch.Groups[4].Value, CultureInfo.InvariantCulture);
            var statTerms = enchantmentMatch.Groups[5].Value.Replace(" ", string.Empty).Split(',', StringSplitOptions.RemoveEmptyEntries);

            int counter = 0;
            foreach (var statTerm in statTerms)
            {
                var enchantmentIdentifier = enchantmentIdentifierPrefix + "_" + (startCount + counter);
                var enchantmentDefinitionDto = new EnchantmentDefinitionDto(
                    enchantmentIdentifier,
                    statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm(statTerm).StatDefinitionId.ToString(),
                    statTerm,
                    modifier,
                    rangeMin,
                    rangeMax,
                    decimalPlaces);
                yield return enchantmentDefinitionDto;
                counter++;
            }
        }
    }
}
