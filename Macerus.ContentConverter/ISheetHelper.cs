using System.Collections.Generic;

using NPOI.SS.UserModel;

namespace Macerus.ContentConverter
{
    public interface ISheetHelper
    {
        IReadOnlyDictionary<string, int> GetColumnHeaderMapping(IRow row);

        int GetIntValue(IRow row, int columnIndex);

        string GetStringValue(IRow row, int columnIndex);

        bool TryGetIntValue(
            IRow row,
            int columnIndex,
            out int value);

        bool TryGetDoubleValue(
            IRow row,
            int columnIndex,
            out double value);
    }
}