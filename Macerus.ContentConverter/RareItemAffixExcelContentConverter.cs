using System;
using System.Collections.Generic;
using System.Globalization;

using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

using ProjectXyz.Plugins.Features.Stats;

namespace Macerus.ContentConverter
{
    public sealed class RareItemAffixExcelContentConverter
    {
        private readonly ISheetHelper _sheetHelper;

        public RareItemAffixExcelContentConverter(ISheetHelper sheetHelper)
        {
            _sheetHelper = sheetHelper;
        }

        public IEnumerable<RareItemAffixesContent> GetRareItemAffixesContent(
            XSSFWorkbook workbook,
            IReadOnlyStatDefinitionToTermMappingRepository statDefinitionToTermMappingRepository)
        {
            var sheet = workbook.GetSheet("Rare Item Affixes");
            var columnHeaderMapping = _sheetHelper.GetColumnHeaderMapping(sheet.GetRow(0));
            return GetRareItemAffixesContent(
                sheet,
                columnHeaderMapping,
                statDefinitionToTermMappingRepository);
        }

        private IEnumerable<RareItemAffixesContent> GetRareItemAffixesContent(
            ISheet sheet,
            IReadOnlyDictionary<string, int> columnHeaderMapping,
            IReadOnlyStatDefinitionToTermMappingRepository statDefinitionToTermMappingRepository)
        {
            for (int rowIndex = 1; rowIndex < sheet.PhysicalNumberOfRows; rowIndex++)
            {
                var row = sheet.GetRow(rowIndex);

                var itemNameStringResource = row.GetCell(columnHeaderMapping["name"]).StringCellValue;
                var itemNameStringResourceId = $"rare_item_affix_{rowIndex}";
                var itemNameStringResourceDto = new StringResourceDto(
                    itemNameStringResourceId,
                    itemNameStringResource);

                var isPrefix = Convert.ToBoolean(
                    row.GetCell(columnHeaderMapping["Is Prefix"]).CellType == CellType.Numeric
                        ? (object)row.GetCell(columnHeaderMapping["Is Prefix"]).NumericCellValue
                        : (object)row.GetCell(columnHeaderMapping["Is Prefix"]).StringCellValue,
                    CultureInfo.InvariantCulture);

                var requiredTagsString = row
                    .GetCell(columnHeaderMapping["required tags"])
                    ?.StringCellValue
                    ?? string.Empty;
                var requiredTags = requiredTagsString.Split(
                    ",",
                    StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                var rareItemAffixDto = new RareItemAffixDto(
                    itemNameStringResourceId,
                    itemNameStringResource,
                    isPrefix,
                    requiredTags);
                yield return new RareItemAffixesContent(
                    rareItemAffixDto,
                    itemNameStringResourceDto);
            }
        }
    }
}
