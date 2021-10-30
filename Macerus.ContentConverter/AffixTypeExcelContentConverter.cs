using System.Collections.Generic;

using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Macerus.ContentConverter
{
    public sealed class AffixTypeExcelContentConverter
    {
        private readonly ISheetHelper _sheetHelper;

        public AffixTypeExcelContentConverter(ISheetHelper sheetHelper)
        {
            _sheetHelper = sheetHelper;
        }

        public IEnumerable<AffixTypeDto> GetAffixContent(XSSFWorkbook workbook)
        {
            var sheet = workbook.GetSheet("Affix Types");
            var columnHeaderMapping = _sheetHelper.GetColumnHeaderMapping(sheet.GetRow(0));
            return GetAffixTypeContent(
                sheet,
                columnHeaderMapping);
        }

        private IEnumerable<AffixTypeDto> GetAffixTypeContent(
            ISheet sheet,
            IReadOnlyDictionary<string, int> columnHeaderMapping)
        {
            for (int rowIndex = 1; rowIndex < sheet.PhysicalNumberOfRows; rowIndex++)
            {
                var row = sheet.GetRow(rowIndex);

                var affixId = $"affix_type_{rowIndex}";
                var name = row.GetCell(columnHeaderMapping["name"])?.StringCellValue;

                yield return new AffixTypeDto(
                    affixId,
                    name);
            }
        }
    }
}
