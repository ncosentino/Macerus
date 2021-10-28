using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Resources.Default
{
    public sealed class ImageResourceRepositoryFacade : IImageResourceRepositoryFacade
    {
        private readonly Lazy<IReadOnlyCollection<IDiscoverableImageResourceRepository>> _lazyRepositories;

        public ImageResourceRepositoryFacade(Lazy<IEnumerable<IDiscoverableImageResourceRepository>> lazyRepositories)
        {
            _lazyRepositories = new Lazy<IReadOnlyCollection<IDiscoverableImageResourceRepository>>(() =>
                lazyRepositories.Value.ToArray());
        }

        public Stream OpenStreamForResource(IIdentifier imageResourceId)
        {
            var result = _lazyRepositories
                .Value
                .Select(repo => repo.OpenStreamForResource(imageResourceId))
                .FirstOrDefault(x => x != null);

            if (result == null)
            {
                throw new KeyNotFoundException(
                    $"Could not find resource with ID '{imageResourceId}'.");
            }

            return result;
        }
    }
}
