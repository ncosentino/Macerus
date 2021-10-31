using System;
using System.Collections.Generic;
using System.Globalization;

using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Macerus.ContentConverter
{
    public sealed class DropTableExcelContentConverter
    {
        private readonly ISheetHelper _sheetHelper;

        public DropTableExcelContentConverter(ISheetHelper sheetHelper)
        {
            _sheetHelper = sheetHelper;
        }

        public IEnumerable<DropTableDto> GetDropTableContent(XSSFWorkbook workbook)
        {
            var sheet = workbook.GetSheet("Drop Tables");
            var columnHeaderMapping = _sheetHelper.GetColumnHeaderMapping(sheet.GetRow(0));
            return GetDropTableContent(
                sheet,
                columnHeaderMapping);
        }

        private IEnumerable<DropTableDto> GetDropTableContent(
            ISheet sheet,
            IReadOnlyDictionary<string, int> columnHeaderMapping)
        {
            for (int rowIndex = 1; rowIndex < sheet.PhysicalNumberOfRows; rowIndex++)
            {
                var row = sheet.GetRow(rowIndex);

                var dropTableId = row.GetCell(columnHeaderMapping["id"])?.StringCellValue;
                if (string.IsNullOrWhiteSpace(dropTableId))
                {
                    Console.WriteLine(
                        $"ERROR: Drop table at row {rowIndex} did not have an ID specified. Skipping...");
                    continue;
                }

                int minimumDrop = (int)row.GetCell(columnHeaderMapping["Minimum Drop"])?.NumericCellValue;
                int maximumDrop = (int)row.GetCell(columnHeaderMapping["Maximum Drop"])?.NumericCellValue;

                if (minimumDrop < 0 ||
                    maximumDrop <= 0 ||
                    minimumDrop > maximumDrop)
                {
                    Console.WriteLine(
                        $"ERROR: Drop table '{dropTableId}' must have 0 <= MIN " +
                        $"<= 1 <= MAX but minimum was {minimumDrop} and maximum " +
                        $"was {maximumDrop}. Skipping...");
                    continue;
                }

                int requiredItemLevel = (int)row.GetCell(columnHeaderMapping["Required Item Level"])?.NumericCellValue;
                int? providedItemLevel = (int?)row.GetCell(columnHeaderMapping["Provided Item Level"])?.NumericCellValue;

                var providedAffixTypes = (row
                        .GetCell(columnHeaderMapping["Provided Affix Types"])
                        ?.StringCellValue
                        ?? string.Empty)
                    .Split(
                        ',',
                        StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                var rawLinkedTables = row.GetCell(columnHeaderMapping["Linked Tables"])?.StringCellValue;
                var linkedTables = new List<KeyValuePair<string, double>>();
                if (!string.IsNullOrWhiteSpace(rawLinkedTables))
                {
                    rawLinkedTables = rawLinkedTables.Replace("\r\n", string.Empty);
                    var splitLinkedTables = rawLinkedTables.Split(
                        ",",
                        StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                    if (splitLinkedTables.Length % 2 != 0)
                    {
                        Console.WriteLine(
                            $"ERROR: Drop table '{dropTableId}' did not have " +
                            $"parsable Linked Tables. There must be a weight,ID" +
                            $",weight,ID,etc... format used. Skipping...");
                        continue;
                    }
                    
                    for (int linkedTableIndex = 0; linkedTableIndex < splitLinkedTables.Length; linkedTableIndex += 2)
                    {
                        linkedTables.Add(new KeyValuePair<string, double>(
                            splitLinkedTables[linkedTableIndex + 1],
                            double.Parse(splitLinkedTables[linkedTableIndex], CultureInfo.InvariantCulture)));
                    }
                }


                yield return new DropTableDto(
                    dropTableId,
                    minimumDrop,
                    maximumDrop,
                    requiredItemLevel,
                    providedItemLevel,
                    providedAffixTypes,
                    linkedTables);
            }
        }
    }
}
