using System;
using System.Collections.Generic;
using System.Globalization;

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

        public string GetStringValue(
            IRow row, 
            int columnIndex)
        {
            var cell = row.GetCell(columnIndex);
            if (cell == null)
            {
                return null;
            }

            if (cell.CellType == CellType.Numeric)
            {
                return cell.NumericCellValue.ToString(CultureInfo.InvariantCulture);
            }

            if (cell.CellType == CellType.Formula)
            {
                try
                {
                    return cell.NumericCellValue.ToString(CultureInfo.InvariantCulture);
                }
                catch { }
            }

            return cell.StringCellValue;
        }

        public bool TryGetIntValue(
            IRow row,
            int columnIndex,
            out int value)
        {
            value = 0;

            var cell = row.GetCell(columnIndex);
            if (cell?.CellType != CellType.Numeric)
            {
                return false;
            }

            value = (int)cell.NumericCellValue;
            return true;
        }

        public bool TryGetDoubleValue(
           IRow row,
           int columnIndex,
           out double value)
        {
            value = 0;

            var cell = row.GetCell(columnIndex);
            if (cell?.CellType != CellType.Numeric)
            {
                return false;
            }

            value = cell.NumericCellValue;
            return true;
        }
    }
}
