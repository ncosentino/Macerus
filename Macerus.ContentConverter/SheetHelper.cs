using System;
using System.Collections.Generic;

using NPOI.SS.UserModel;

namespace Macerus.ContentConverter
{
    public sealed class SheetHelper : ISheetHelper
    {
        public IReadOnlyDictionary<string, int> GetColumnHeaderMapping(IRow row)
        {
            var mapping = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < row.Cells.Count; i++)
            {
                mapping[row.GetCell(i).StringCellValue] = i;
            }

            return mapping;
        }

        public int GetIntValue(IRow row, int columnIndex)
        {
            var cell = row.GetCell(columnIndex);
            var result = cell?.CellType == CellType.Numeric
                ? (int)cell.NumericCellValue
                : 0;
            return result;
        }
    }
}
