using Macerus.Plugins.Features.Gui;

namespace Macerus.Plugins.Features.Hud
{
    public interface IHudWindowViewModel : IWindowViewModel
    {
        bool IsLeftDocked { get; }
    }
}
