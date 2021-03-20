using System.IO;

using ProjectXyz.Api.Logging;

namespace Macerus.Plugins.Features.Mapping.TiledNet
{
    public sealed class MapResourceIdConverter : IMapResourceIdConverter
    {
        private readonly string _relativeMapsResourceRoot;
        private readonly ILogger _logger;

        public MapResourceIdConverter(
            IMappingAssetPaths assetPaths,
            ILogger logger)
        {
            _logger = logger;
            _relativeMapsResourceRoot = assetPaths
                .MapsRoot
                .Substring(assetPaths.ResourcesRoot.Length)
                .TrimStart('\\', '/');
        }

        public string Convert(string mapResourceId)
        {
            _logger.Debug($"Map resource id {mapResourceId}");
            return Path.Combine(_relativeMapsResourceRoot, $"{mapResourceId}_tmx");
        }
    }
}