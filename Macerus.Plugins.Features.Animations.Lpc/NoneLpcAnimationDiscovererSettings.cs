namespace Macerus.Plugins.Features.Animations.Lpc
{
    public sealed class NoneLpcAnimationDiscovererSettings : ILpcAnimationDiscovererSettings
    {
        public string LcpUniversalPath { get; } = string.Empty;

        public string RelativeLcpSpriteSheetPath { get; } = string.Empty;
    }
}
