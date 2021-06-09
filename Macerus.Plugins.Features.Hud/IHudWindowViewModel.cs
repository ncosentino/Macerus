using Macerus.Plugins.Features.Gui.Api;

namespace Macerus.Plugins.Features.Hud
{
    public interface IHudWindowViewModel : IWindowViewModel
    {
        bool IsLeftDocked { get; }
    }
}
