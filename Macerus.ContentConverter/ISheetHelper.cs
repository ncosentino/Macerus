using System.Collections.Generic;

using NPOI.SS.UserModel;

namespace Macerus.ContentConverter
{
    public interface ISheetHelper
    {
        IReadOnlyDictionary<string, int> GetColumnHeaderMapping(IRow row);
        int GetIntValue(IRow row, int columnIndex);
    }
}