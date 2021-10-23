using NPOI.XSSF.UserModel;

using ProjectXyz.Plugins.Features.Stats;

namespace Macerus.ContentConverter
{
    public interface IUniqueItemExcelContentConverter
    {
        void ConvertUniqueItems(XSSFWorkbook workbook, IReadOnlyStatDefinitionToTermMappingRepository statDefinitionToTermMappingRepository);
    }
}