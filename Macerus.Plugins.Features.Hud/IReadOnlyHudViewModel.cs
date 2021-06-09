using System.Collections.Generic;

namespace Macerus.Plugins.Features.Hud
{
    public interface IReadOnlyHudViewModel
    {
        IReadOnlyCollection<IHudWindowViewModel> Windows { get; }
    }
}
