using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Resources.Default
{
    public sealed class StringResourceRepositoryFacade : IStringResourceRepositoryFacade
    {
        private readonly Lazy<IReadOnlyCollection<IDiscoverableStringResourceRepository>> _lazyRepositories;

        public StringResourceRepositoryFacade(Lazy<IEnumerable<IDiscoverableStringResourceRepository>> lazyRepositories)
        {
            _lazyRepositories = new Lazy<IReadOnlyCollection<IDiscoverableStringResourceRepository>>(() =>
                lazyRepositories.Value.ToArray());
        }

        public CultureInfo CurrentCulture { get; } = CultureInfo.InvariantCulture;

        public string GetString(
            IIdentifier stringResourceId,
            CultureInfo culture)
        {
            var result = _lazyRepositories
                .Value
                .Select(repo => repo
                    .GetString(
                        stringResourceId,
                        culture))
                .FirstOrDefault(x => x != null);

            if (result == null)
            {
                throw new KeyNotFoundException(
                    $"Could not find resource with ID '{stringResourceId}'.");
            }

            return result;
        }
    }
}
