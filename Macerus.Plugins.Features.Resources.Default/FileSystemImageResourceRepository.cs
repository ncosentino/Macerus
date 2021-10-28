using System.IO;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Resources.Default
{
    public sealed class FileSystemImageResourceRepository : IDiscoverableImageResourceRepository
    {
        private readonly string _resourceDirectory;

        public FileSystemImageResourceRepository(string resourceDirectory)
        {
            _resourceDirectory = resourceDirectory;
        }

        public Stream OpenStreamForResource(IIdentifier imageResourceId)
        {
            var filePath = Path.Combine(_resourceDirectory, imageResourceId.ToString());
            if (!File.Exists(filePath))
            {
                return null;
            }

            return File.OpenRead(filePath);
        }
    }
}
