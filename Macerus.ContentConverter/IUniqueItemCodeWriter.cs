using System.Collections.Generic;

namespace Macerus.ContentConverter
{
    public interface IUniqueItemCodeWriter
    {
        void WriteUniqueItemsCode(IEnumerable<UniqueItemDto> uniqueItemDtos);
    }
}