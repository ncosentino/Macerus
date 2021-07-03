namespace Macerus.Plugins.Features.Minimap
{
    public interface IMinimapController
    {
        bool ShowingMinimapBadge { get; }

        bool ShowingMinimapOverlay { get; }

        void HideMinimap();

        void ShowMinimap(bool showBadge);
    }
}
