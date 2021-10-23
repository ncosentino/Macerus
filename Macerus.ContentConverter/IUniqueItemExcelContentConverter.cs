using System.Collections.Generic;

using NPOI.XSSF.UserModel;

using ProjectXyz.Plugins.Features.Stats;

namespace Macerus.ContentConverter
{
    public interface IUniqueItemExcelContentConverter
    {
        void WriteUniqueItemsCode(IEnumerable<UniqueItemDto> uniqueItemDtos);

        IEnumerable<UniqueItemConvertedContent> GetUniqueItemContent(
            XSSFWorkbook workbook,
            IReadOnlyStatDefinitionToTermMappingRepository statDefinitionToTermMappingRepository);
    }
}