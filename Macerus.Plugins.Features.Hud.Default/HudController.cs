using System;
using System.Linq;

namespace Macerus.Plugins.Features.Hud.Default
{
    public sealed class HudController : IHudController
    {
        private readonly IHudViewModel _hudViewModel;

        public HudController(IHudViewModel hudViewModel)
        {
            _hudViewModel = hudViewModel;

            foreach (var hudWindowViewModel in hudViewModel.Windows)
            {
                hudWindowViewModel.Opened += HudWindowViewModel_Opened;
            }
        }

        public void CloseAllWindows() => _hudViewModel.CloseAllWindows();

        private void HudWindowViewModel_Opened(
            object sender,
            EventArgs e)
        {
            var viewModel = (IHudWindowViewModel)sender;
            foreach (var otherViewModel in _hudViewModel
                .Windows
                .Where(vm => vm.IsLeftDocked == viewModel.IsLeftDocked)
                .Where(vm => vm != viewModel))
            {
                otherViewModel.Close();
            }
        }
    }
}
