using System.IO;

namespace Macerus.Plugins.Features.Mapping.Default
{
    public sealed class MapResourceIdConverter : IMapResourceIdConverter
    {
        private readonly string _relativeMapsResourceRoot;

        public MapResourceIdConverter(IMappingAssetPaths assetPaths)
        {
            _relativeMapsResourceRoot = assetPaths
                .MapsRoot
                .Substring(assetPaths.ResourcesRoot.Length)
                .TrimStart('\\', '/');
        }

        public string ConvertToMapResourcePath(string mapResourceId)
        {
            return Path.Combine(_relativeMapsResourceRoot, $"{mapResourceId}");
        }

        public string ConvertToGameObjectResourcePath(string mapResourceId)
        {
            return Path.Combine(_relativeMapsResourceRoot, $"{mapResourceId}.objects");
        }
    }
}