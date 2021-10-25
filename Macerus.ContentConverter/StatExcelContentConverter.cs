using System.Collections.Generic;

using NPOI.XSSF.UserModel;

namespace Macerus.ContentConverter
{
    public sealed class StatExcelContentConverter
    {
        private readonly ISheetHelper _sheetHelper;

        public StatExcelContentConverter(ISheetHelper sheetHelper)
        {
            _sheetHelper = sheetHelper;
        }

        public IEnumerable<StatDto> ConvertStats(XSSFWorkbook workbook)
        {
            var statsSheet = workbook.GetSheet("Stats");
            var columnHeaderMapping = _sheetHelper.GetColumnHeaderMapping(statsSheet.GetRow(0));

            for (int rowIndex = 1; rowIndex < statsSheet.PhysicalNumberOfRows; rowIndex++)
            {
                var row = statsSheet.GetRow(rowIndex);
                var statDefinitionId = $"stat_{rowIndex}";

                var statTerm = row.GetCell(columnHeaderMapping["term"]).StringCellValue;

                var statDto = new StatDto(
                    statDefinitionId,
                    statTerm);
                yield return statDto;
            }
        }
    }
}
