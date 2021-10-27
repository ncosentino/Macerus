using System.Collections.Generic;

namespace Macerus.ContentConverter
{
    public sealed class BaseArmorConvertedContent
    {
        public BaseArmorConvertedContent(
            IReadOnlyCollection<BaseArmorDto> baseArmorDtos,
            IReadOnlyCollection<StringResourceDto> stringResourceDtos,
            IReadOnlyCollection<ImageResourceDto> imageResourceDtos)
        {
            BaseArmorDtos = baseArmorDtos;
            StringResourceDtos = stringResourceDtos;
            ImageResourceDtos = imageResourceDtos;
        }

        public IReadOnlyCollection<BaseArmorDto> BaseArmorDtos { get; }

        public IReadOnlyCollection<StringResourceDto> StringResourceDtos { get; }

        public IReadOnlyCollection<ImageResourceDto> ImageResourceDtos { get; }
    }
}
