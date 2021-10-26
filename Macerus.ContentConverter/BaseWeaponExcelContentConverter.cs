using System;
using System.Collections.Generic;

using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Macerus.ContentConverter
{
    public sealed class BaseWeaponExcelContentConverter
    {
        private readonly ISheetHelper _sheetHelper;

        public BaseWeaponExcelContentConverter(ISheetHelper sheetHelper)
        {
            _sheetHelper = sheetHelper;
        }

        public BaseWeaponConvertedContent ConvertBaseWeapons(XSSFWorkbook workbook)
        {
            var sheet = workbook.GetSheet("Base Weapons");
            var columnHeaderMapping = _sheetHelper.GetColumnHeaderMapping(sheet.GetRow(0));

            var baseWeaponConvertedContent = GetBaseWeaponConvertedContent(
                sheet,
                columnHeaderMapping);
            return baseWeaponConvertedContent;
        }

        private BaseWeaponConvertedContent GetBaseWeaponConvertedContent(
            ISheet sheet,
            IReadOnlyDictionary<string, int> columnHeaderMapping)
        {
            var baseWeaponDtos = new List<BaseWeaponDto>();
            var stringResourceDtos = new List<StringResourceDto>();
            var imageResourceDtos = new List<ImageResourceDto>();

            for (int rowIndex = 1; rowIndex < sheet.PhysicalNumberOfRows; rowIndex++)
            {
                var row = sheet.GetRow(rowIndex);
                var itemId = $"weapon_{rowIndex}";

                var itemNameStringResource = row.GetCell(columnHeaderMapping["name"]).StringCellValue;
                var itemNameStringResourceId = $"weapon_name_{rowIndex}";
                stringResourceDtos.Add(new StringResourceDto(
                    itemNameStringResourceId,
                    itemNameStringResource));

                var itemIconResource = row.GetCell(columnHeaderMapping["icon"]).StringCellValue;
                var itemIconResourceId = $"weapon_icon_{rowIndex}";
                imageResourceDtos.Add(new ImageResourceDto(
                    itemIconResource,
                    itemIconResourceId));

                var tags = (row
                    .GetCell(columnHeaderMapping["Tags"])
                    ?.StringCellValue
                    ?? string.Empty)
                    .Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                var itemEquipSlotName = row.GetCell(columnHeaderMapping["slot"]).StringCellValue; // FIXME: Split for multi-slot?
                var itemEquipSlotId = itemEquipSlotName; // FIXME: convert???

                var itemLevel = _sheetHelper.GetIntValue(row, columnHeaderMapping["item level"]);

                var itemAttackSpeed = _sheetHelper.GetIntValue(row, columnHeaderMapping["attack speed"]);
                var itemRange = _sheetHelper.GetIntValue(row, columnHeaderMapping["range"]);

                var itemLevelRequirement = _sheetHelper.GetIntValue(row, columnHeaderMapping["level requirement"]);
                var itemStrengthRequirement = _sheetHelper.GetIntValue(row, columnHeaderMapping["req str"]);
                var itemDexterityRequirement = _sheetHelper.GetIntValue(row, columnHeaderMapping["req dex"]);
                var itemIntelligenceRequirement = _sheetHelper.GetIntValue(row, columnHeaderMapping["req int"]);
                var itemSpeedRequirement = _sheetHelper.GetIntValue(row, columnHeaderMapping["req spd"]);
                var itemVitalityRequirement = _sheetHelper.GetIntValue(row, columnHeaderMapping["req vit"]);

                var itemPhysicalDamageMinMin = _sheetHelper.GetIntValue(row, columnHeaderMapping["physical damage min min"]);
                var itemPhysicalDamageMinMax = _sheetHelper.GetIntValue(row, columnHeaderMapping["physical damage min max"]);
                var itemPhysicalDamageMaxMin = _sheetHelper.GetIntValue(row, columnHeaderMapping["physical damage max min"]);
                var itemPhysicalDamageMaxMax = _sheetHelper.GetIntValue(row, columnHeaderMapping["physical damage max max"]);
                var itemMagicDamageMinMin = _sheetHelper.GetIntValue(row, columnHeaderMapping["magic damage min min"]);
                var itemMagicDamageMinMax = _sheetHelper.GetIntValue(row, columnHeaderMapping["magic damage min max"]);
                var itemMagicDamageMaxMin = _sheetHelper.GetIntValue(row, columnHeaderMapping["magic damage max min"]);
                var itemMagicDamageMaxMax = _sheetHelper.GetIntValue(row, columnHeaderMapping["magic damage max max"]);

                var itemDurabilityMinimum = _sheetHelper.GetIntValue(row, columnHeaderMapping["durability min"]);
                var itemDurabilityMaximum = _sheetHelper.GetIntValue(row, columnHeaderMapping["durability max"]);

                var itemSocketsMinimum = _sheetHelper.GetIntValue(row, columnHeaderMapping["sockets min"]);
                var itemSocketsMaximum = _sheetHelper.GetIntValue(row, columnHeaderMapping["sockets max"]);

                baseWeaponDtos.Add(new BaseWeaponDto(
                    itemId,
                    itemNameStringResource,
                    itemNameStringResourceId,
                    itemIconResource,
                    itemIconResourceId,
                    itemEquipSlotName,
                    itemEquipSlotId,
                    itemLevel,
                    itemAttackSpeed,
                    itemRange,
                    itemLevelRequirement,
                    itemStrengthRequirement,
                    itemDexterityRequirement,
                    itemIntelligenceRequirement,
                    itemSpeedRequirement,
                    itemVitalityRequirement,
                    itemPhysicalDamageMinMin,
                    itemPhysicalDamageMinMax,
                    itemPhysicalDamageMaxMin,
                    itemPhysicalDamageMaxMax,
                    itemMagicDamageMinMin,
                    itemMagicDamageMinMax,
                    itemMagicDamageMaxMin,
                    itemMagicDamageMaxMax,
                    itemDurabilityMinimum,
                    itemDurabilityMaximum,
                    itemSocketsMinimum,
                    itemSocketsMaximum,
                    tags));
            }

            var baseWeaponConvertedContent = new BaseWeaponConvertedContent(
                baseWeaponDtos,
                stringResourceDtos,
                imageResourceDtos);
            return baseWeaponConvertedContent;
        }
    }
}
