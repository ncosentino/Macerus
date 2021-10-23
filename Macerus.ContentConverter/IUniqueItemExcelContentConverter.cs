using System.Collections.Generic;

using NPOI.XSSF.UserModel;

using ProjectXyz.Plugins.Features.Stats;

namespace Macerus.ContentConverter
{
    public interface IUniqueItemExcelContentConverter
    {
        IEnumerable<UniqueItemConvertedContent> GetUniqueItemContent(
            XSSFWorkbook workbook,
            IReadOnlyStatDefinitionToTermMappingRepository statDefinitionToTermMappingRepository);
    }
}