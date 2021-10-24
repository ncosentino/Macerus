using System.Collections.Generic;

namespace Macerus.ContentConverter
{
    public interface IStringResourceCodeWriter
    {
        void WriteStringResourceModule(
            IEnumerable<StringResourceDto> stringResourceDtos,
            string outputDirectory);
    }
}