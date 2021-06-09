using System.Collections.Generic;
using System.Linq;

namespace Macerus.Plugins.Features.Hud.Default
{
    public sealed class HudViewModel : IHudViewModel
    {
        private readonly IReadOnlyCollection<IHudWindowViewModel> _hudWindowViewModels;

        public HudViewModel(IEnumerable<IDiscoverableHudWindowViewModel> hudWindowViewModels)
        {
            _hudWindowViewModels = hudWindowViewModels.ToArray();
        }

        public IReadOnlyCollection<IHudWindowViewModel> Windows => _hudWindowViewModels;

        public void CloseAllWindows()
        {
            foreach (var viewModel in _hudWindowViewModels)
            {
                viewModel.Close();
            }
        }
    }
}
