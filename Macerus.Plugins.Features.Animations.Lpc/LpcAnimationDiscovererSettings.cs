using System.IO;

namespace Macerus.Plugins.Features.Animations.Lpc
{
    public sealed class LpcAnimationDiscovererSettings : ILpcAnimationDiscovererSettings
    {
        private readonly string _resourcesRoot;

        public LpcAnimationDiscovererSettings(
            string resourcesRoot,
            string relativeLcpSpriteSheetPath)
        {
            _resourcesRoot = resourcesRoot;
            RelativeLcpSpriteSheetPath = relativeLcpSpriteSheetPath;
        }

        public string LcpUniversalPath => Path.Combine(
            _resourcesRoot,
            RelativeLcpSpriteSheetPath);

        public string RelativeLcpSpriteSheetPath { get; }
    }
}
