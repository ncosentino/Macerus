using System.Collections.Generic;

namespace Macerus.ContentConverter
{
    public sealed class StatContent
    {
        public StatContent(
            IReadOnlyCollection<StatDto> statDtos,
            IReadOnlyCollection<StatBoundsDto> statBoundsDtos,
            IReadOnlyCollection<StringResourceDto> stringResourceDtos)
        {
            StatDtos = statDtos;
            StatBoundsDtos = statBoundsDtos;
            StringResourceDtos = stringResourceDtos;
        }

        public IReadOnlyCollection<StatDto> StatDtos { get; }

        public IReadOnlyCollection<StatBoundsDto> StatBoundsDtos { get; }

        public IReadOnlyCollection<StringResourceDto> StringResourceDtos { get; }
    }
}
