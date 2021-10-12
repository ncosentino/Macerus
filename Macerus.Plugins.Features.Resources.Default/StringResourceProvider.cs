using System.Globalization;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Resources.Default
{
    public sealed class StringResourceProvider : IStringResourceProvider
    {
        private readonly IStringResourceRepositoryFacade _stringResourceRepository;

        public StringResourceProvider(IStringResourceRepositoryFacade stringResourceRepository)
        {
            _stringResourceRepository = stringResourceRepository;
        }

        public CultureInfo CurrentCulture { get; } = CultureInfo.InvariantCulture;

        public string GetString(
            IIdentifier stringResourceId,
            CultureInfo culture) =>
            _stringResourceRepository.GetString(stringResourceId, culture);
    }
}
