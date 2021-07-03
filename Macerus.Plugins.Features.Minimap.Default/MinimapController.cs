namespace Macerus.Plugins.Features.Minimap.Default
{
    public sealed class MinimapController : IMinimapController
    {
        private readonly IMinimapOverlayViewModel _minimapOverlayViewModel;
        private readonly IMinimapBadgeViewModel _minimapBadgeViewModel;

        public MinimapController(
            IMinimapOverlayViewModel minimapOverlayViewModel,
            IMinimapBadgeViewModel minimapBadgeViewModel)
        {
            _minimapOverlayViewModel = minimapOverlayViewModel;
            _minimapBadgeViewModel = minimapBadgeViewModel;
        }

        public bool ShowingMinimapBadge => _minimapBadgeViewModel.IsOpen;

        public bool ShowingMinimapOverlay => _minimapOverlayViewModel.IsOpen;

        public void ShowMinimap(bool showBadge)
        {
            if (showBadge)
            {
                _minimapBadgeViewModel.Open();
                _minimapOverlayViewModel.Close();
            }
            else
            {
                _minimapBadgeViewModel.Close();
                _minimapOverlayViewModel.Open();
            }
        }

        public void HideMinimap()
        {
            _minimapBadgeViewModel.Close();
            _minimapOverlayViewModel.Close();
        }        
    }
}
