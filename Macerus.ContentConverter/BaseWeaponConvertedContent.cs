using System.Collections.Generic;

namespace Macerus.ContentConverter
{
    public sealed class BaseWeaponConvertedContent
    {
        public BaseWeaponConvertedContent(
            IReadOnlyCollection<BaseWeaponDto> baseWeaponDtos, 
            IReadOnlyCollection<StringResourceDto> stringResourceDtos, 
            IReadOnlyCollection<ImageResourceDto> imageResourceDtos)
        {
            BaseWeaponDtos = baseWeaponDtos;
            StringResourceDtos = stringResourceDtos;
            ImageResourceDtos = imageResourceDtos;
        }

        public IReadOnlyCollection<BaseWeaponDto> BaseWeaponDtos { get; }

        public IReadOnlyCollection<StringResourceDto> StringResourceDtos { get; }

        public IReadOnlyCollection<ImageResourceDto> ImageResourceDtos { get; }
    }
}
