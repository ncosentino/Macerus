using System;
using System.Collections.Generic;
using System.Globalization;

using NPOI.SS.UserModel;
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

        public StatContent ConvertStats(XSSFWorkbook workbook)
        {
            var statsSheet = workbook.GetSheet("Stats");
            var columnHeaderMapping = _sheetHelper.GetColumnHeaderMapping(statsSheet.GetRow(0));

            var statDtos = new List<StatDto>();
            var statBoundsDtos = new List<StatBoundsDto>();
            var stringResourceDtos = new List<StringResourceDto>();

            for (int rowIndex = 1; rowIndex < statsSheet.PhysicalNumberOfRows; rowIndex++)
            {
                var row = statsSheet.GetRow(rowIndex);
                var statDefinitionId = $"stat_{rowIndex}";

                var statTerm = row.GetCell(columnHeaderMapping["term"]).StringCellValue;

                var boundsCell = row.GetCell(columnHeaderMapping["Min Bounds Expression"]);
                var minBoundsExpression = boundsCell == null
                    ? null
                    : boundsCell.CellType == CellType.Numeric 
                        ? Convert.ToString(boundsCell.NumericCellValue, CultureInfo.InvariantCulture) 
                        : boundsCell.StringCellValue;
                boundsCell = row.GetCell(columnHeaderMapping["Max Bounds Expression"]);
                var maxBoundsExpression = boundsCell == null
                    ? null
                    : boundsCell.CellType == CellType.Numeric
                        ? Convert.ToString(boundsCell.NumericCellValue, CultureInfo.InvariantCulture)
                        : boundsCell.StringCellValue;
                if (!string.IsNullOrWhiteSpace(minBoundsExpression) ||
                    !string.IsNullOrWhiteSpace(maxBoundsExpression))
                {
                    statBoundsDtos.Add(new StatBoundsDto(
                        statDefinitionId,
                        minBoundsExpression,
                        maxBoundsExpression));
                }

                statDtos.Add(new StatDto(
                    statDefinitionId,
                    statTerm));

                var stringResource = row.GetCell(columnHeaderMapping["Name"])?.StringCellValue;
                if (string.IsNullOrEmpty(stringResource))
                {
                    stringResource = $"// FIXME: <{statDefinitionId}, {statTerm}>";
                }

                var stringResourceId = statDefinitionId;
                stringResourceDtos.Add(new StringResourceDto(
                    stringResourceId,
                    stringResource));
            }

            return new StatContent(
                statDtos,
                statBoundsDtos,
                stringResourceDtos);
        }
    }
}
