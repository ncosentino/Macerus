using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

using ProjectXyz.Plugins.Features.Stats;

namespace Macerus.ContentConverter
{
    public sealed class AffixesExcelContentConverter
    {
        private readonly ISheetHelper _sheetHelper;

        public AffixesExcelContentConverter(ISheetHelper sheetHelper)
        {
            _sheetHelper = sheetHelper;
        }

        public IEnumerable<AffixConvertedContent> GetAffixContent(
            XSSFWorkbook workbook,
            IReadOnlyStatDefinitionToTermMappingRepository statDefinitionToTermMappingRepository)
        {
            var sheet = workbook.GetSheet("Affixes");
            var columnHeaderMapping = _sheetHelper.GetColumnHeaderMapping(sheet.GetRow(0));
            return GetAffixContent(
                sheet,
                columnHeaderMapping,
                statDefinitionToTermMappingRepository);
        }

        private IEnumerable<AffixConvertedContent> GetAffixContent(
            ISheet sheet,
            IReadOnlyDictionary<string, int> columnHeaderMapping,
            IReadOnlyStatDefinitionToTermMappingRepository statDefinitionToTermMappingRepository)
        {
            for (int rowIndex = 1; rowIndex < sheet.PhysicalNumberOfRows; rowIndex++)
            {
                var row = sheet.GetRow(rowIndex);

                var affixId = $"item_affix_{rowIndex}";

                var stringResourceDtos = new List<StringResourceDto>();

                var prefixStringResource = row.GetCell(columnHeaderMapping["prefix"])?.StringCellValue;
                var prefixStringResourceId = $"item_prefix_name_{rowIndex}";
                if (!string.IsNullOrWhiteSpace(prefixStringResource))
                {
                    var prefixStringResourceDto = new StringResourceDto(
                        prefixStringResourceId,
                        prefixStringResource);
                    stringResourceDtos.Add(prefixStringResourceDto);
                }

                var suffixStringResource = row.GetCell(columnHeaderMapping["suffix"])?.StringCellValue;
                var suffixStringResourceId = $"item_suffix_name_{rowIndex}";
                if (!string.IsNullOrWhiteSpace(suffixStringResource))
                {
                    var suffixStringResourceDto = new StringResourceDto(
                        suffixStringResourceId,
                        suffixStringResource);
                    stringResourceDtos.Add(suffixStringResourceDto);
                }

                var affixDtos = new List<AffixDto>();
                var enchantmentDefinitionDtos = new List<EnchantmentDefinitionDto>();

                if (stringResourceDtos.Any())
                {
                    var magicAffixAndEnchantments = GetAffixAndEnchantments(
                        row,
                        columnHeaderMapping,
                        statDefinitionToTermMappingRepository,
                        "magic_" + affixId,
                        "Magic",
                        string.IsNullOrWhiteSpace(prefixStringResource) ? null : prefixStringResourceId,
                        string.IsNullOrWhiteSpace(suffixStringResource) ? null : suffixStringResourceId);
                    if (magicAffixAndEnchantments != null)
                    {
                        affixDtos.Add(magicAffixAndEnchantments.Item1);
                        enchantmentDefinitionDtos.AddRange(magicAffixAndEnchantments.Item2);
                    }
                }

                var rareAffixAndEnchantments = GetAffixAndEnchantments(
                    row,
                    columnHeaderMapping,
                    statDefinitionToTermMappingRepository,
                    "rare_" + affixId,
                    "Rare",
                    null,
                    null);
                if (rareAffixAndEnchantments != null)
                {
                    affixDtos.Add(rareAffixAndEnchantments.Item1);
                    enchantmentDefinitionDtos.AddRange(rareAffixAndEnchantments.Item2);
                }

                yield return new AffixConvertedContent(
                    affixDtos,
                    stringResourceDtos,
                    enchantmentDefinitionDtos);
            }
        }

        private Tuple<AffixDto, IReadOnlyCollection<EnchantmentDefinitionDto>> GetAffixAndEnchantments(
            IRow row,
            IReadOnlyDictionary<string, int> columnHeaderMapping,
            IReadOnlyStatDefinitionToTermMappingRepository statDefinitionToTermMappingRepository,
            string affixId,
            string affixType,
            string prefixStringResourceId,
            string suffixStringResourceId)
        {
            var enchantmentDefinitionDtos = new List<EnchantmentDefinitionDto>();

            var minimumLevel = _sheetHelper.GetIntValue(row, columnHeaderMapping["Minimum Level"]);
            var maximumLevel = _sheetHelper.GetIntValue(row, columnHeaderMapping["Maximum Level"]);

            for (int statCellIndex = 1; statCellIndex < 10; statCellIndex++)
            {
                var columnHeader = "Stat " + statCellIndex;
                var statTerm = columnHeaderMapping.ContainsKey(columnHeader)
                    ? row.GetCell(columnHeaderMapping[columnHeader])?.StringCellValue
                    : null;
                if (string.IsNullOrWhiteSpace(statTerm))
                {
                    break;
                }

                var statDefinitionId = statDefinitionToTermMappingRepository
                    .GetStatDefinitionToTermMappingByTerm(statTerm)
                    .StatDefinitionId
                    .ToString();

                columnHeader = $"Stat {statCellIndex} Modifier";
                var modifier = row.GetCell(columnHeaderMapping[columnHeader]).StringCellValue;
                if (string.IsNullOrWhiteSpace(modifier))
                {
                    continue;
                }

                columnHeader = $"Stat {statCellIndex} {affixType} Min";
                if (!_sheetHelper.TryGetDoubleValue(
                    row,
                    columnHeaderMapping[columnHeader],
                    out var minimum))
                {
                    continue;
                }

                columnHeader = $"Stat {statCellIndex} {affixType} Max";
                if (!_sheetHelper.TryGetDoubleValue(
                    row,
                    columnHeaderMapping[columnHeader],
                    out var maximum))
                {
                    continue;
                }

                var decimalPlaces = 0; // FIXME: pull from sheet
                var enchantmentDefinitionDto = new EnchantmentDefinitionDto(
                    $"{affixId}_enchantment_{statCellIndex - 1}",
                    statDefinitionId,
                    statTerm,
                    modifier,
                    minimum,
                    maximum,
                    decimalPlaces);
                enchantmentDefinitionDtos.Add(enchantmentDefinitionDto);
            }

            if (!enchantmentDefinitionDtos.Any())
            {
                return null;
            }

            var affixDto = new AffixDto(
                affixId,
                affixType.ToLowerInvariant(),
                minimumLevel,
                maximumLevel,
                prefixStringResourceId,
                suffixStringResourceId,
                enchantmentDefinitionDtos.Select(x => x.EnchantmentDefinitionId).ToArray());
            return new Tuple<AffixDto, IReadOnlyCollection<EnchantmentDefinitionDto>>(
                affixDto,
                enchantmentDefinitionDtos);
        }
    }
}
