using System.Collections.Generic;

using NPOI.XSSF.UserModel;

using ProjectXyz.Plugins.Features.Stats;

namespace Macerus.ContentConverter
{
    public interface IAffixesExcelContentConverter
    {
        IEnumerable<AffixConvertedContent> GetAffixContent(XSSFWorkbook workbook, IReadOnlyStatDefinitionToTermMappingRepository statDefinitionToTermMappingRepository);
        void WriteAffixesCode(IEnumerable<AffixDto> magicAffixDtos);
    }
}