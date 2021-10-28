using System;
using System.Collections.Generic;

using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Macerus.ContentConverter
{
    public sealed class BaseArmorExcelContentConverter
    {
        private readonly ISheetHelper _sheetHelper;

        public BaseArmorExcelContentConverter(ISheetHelper sheetHelper)
        {
            _sheetHelper = sheetHelper;
        }

        public BaseArmorConvertedContent ConvertBaseArmors(XSSFWorkbook workbook)
        {
            var sheet = workbook.GetSheet("Base Armor");
            var columnHeaderMapping = _sheetHelper.GetColumnHeaderMapping(sheet.GetRow(0));

            var baseArmorConvertedContent = GetBaseArmorConvertedContent(
                sheet,
                columnHeaderMapping);
            return baseArmorConvertedContent;
        }

        private BaseArmorConvertedContent GetBaseArmorConvertedContent(
            ISheet sheet,
            IReadOnlyDictionary<string, int> columnHeaderMapping)
        {
            var baseArmorDtos = new List<BaseArmorDto>();
            var stringResourceDtos = new List<StringResourceDto>();
            var imageResourceDtos = new List<ImageResourceDto>();

            for (int rowIndex = 1; rowIndex < sheet.PhysicalNumberOfRows; rowIndex++)
            {
                var row = sheet.GetRow(rowIndex);
                var itemId = $"armor_{rowIndex}";

                var itemNameStringResource = row.GetCell(columnHeaderMapping["name"]).StringCellValue;
                var itemNameStringResourceId = $"armor_name_{rowIndex}";
                stringResourceDtos.Add(new StringResourceDto(
                    itemNameStringResourceId,
                    itemNameStringResource));

                var itemIconResourcePath = row.GetCell(columnHeaderMapping["icon"]).StringCellValue;
                var itemIconResourceDto = new ImageResourceDto(itemIconResourcePath);
                imageResourceDtos.Add(itemIconResourceDto);

                var tags = (row
                    .GetCell(columnHeaderMapping["Tags"])
                    ?.StringCellValue
                    ?? string.Empty)
                    .Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                var itemEquipSlotName = row.GetCell(columnHeaderMapping["slot"]).StringCellValue; // FIXME: Split for multi-slot?
                var itemEquipSlotId = itemEquipSlotName; // FIXME: convert???

                var itemLevel = _sheetHelper.GetIntValue(row, columnHeaderMapping["item level"]);

                var itemBlock = _sheetHelper.GetIntValue(row, columnHeaderMapping["block"]);

                var itemLevelRequirement = _sheetHelper.GetIntValue(row, columnHeaderMapping["level requirement"]);
                var itemStrengthRequirement = _sheetHelper.GetIntValue(row, columnHeaderMapping["req str"]);
                var itemDexterityRequirement = _sheetHelper.GetIntValue(row, columnHeaderMapping["req dex"]);
                var itemIntelligenceRequirement = _sheetHelper.GetIntValue(row, columnHeaderMapping["req int"]);
                var itemSpeedRequirement = _sheetHelper.GetIntValue(row, columnHeaderMapping["req spd"]);
                var itemVitalityRequirement = _sheetHelper.GetIntValue(row, columnHeaderMapping["req vit"]);

                var itemArmorMinimum = _sheetHelper.GetIntValue(row, columnHeaderMapping["armor min"]);
                var itemArmorMaximum = _sheetHelper.GetIntValue(row, columnHeaderMapping["armor max"]);
                var itemEvasionMinimum = _sheetHelper.GetIntValue(row, columnHeaderMapping["evasion min"]);
                var itemEvasionMaximum = _sheetHelper.GetIntValue(row, columnHeaderMapping["evasion max"]);

                var itemDurabilityMinimum = _sheetHelper.GetIntValue(row, columnHeaderMapping["durability min"]);
                var itemDurabilityMaximum = _sheetHelper.GetIntValue(row, columnHeaderMapping["durability max"]);

                var itemSocketsMinimum = _sheetHelper.GetIntValue(row, columnHeaderMapping["sockets min"]);
                var itemSocketsMaximum = _sheetHelper.GetIntValue(row, columnHeaderMapping["sockets max"]);

                baseArmorDtos.Add(new BaseArmorDto(
                    itemId,
                    itemNameStringResource,
                    itemNameStringResourceId,
                    itemIconResourceDto,
                    itemEquipSlotName,
                    itemEquipSlotId,
                    itemLevel,
                    itemBlock,
                    itemLevelRequirement,
                    itemStrengthRequirement,
                    itemDexterityRequirement,
                    itemIntelligenceRequirement,
                    itemSpeedRequirement,
                    itemVitalityRequirement,
                    itemArmorMinimum,
                    itemArmorMaximum,
                    itemEvasionMinimum,
                    itemEvasionMaximum,
                    itemDurabilityMinimum,
                    itemDurabilityMaximum,
                    itemSocketsMinimum,
                    itemSocketsMaximum,
                    tags));
            }

            var baseArmorConvertedContent = new BaseArmorConvertedContent(
                baseArmorDtos,
                stringResourceDtos,
                imageResourceDtos);
            return baseArmorConvertedContent;
        }
    }
}
