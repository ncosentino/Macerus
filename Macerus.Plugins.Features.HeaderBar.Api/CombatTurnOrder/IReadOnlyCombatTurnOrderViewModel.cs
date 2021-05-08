using System.Collections.Generic;

using Macerus.Plugins.Features.Gui.Api;

namespace Macerus.Plugins.Features.HeaderBar.Api.CombatTurnOrder
{
    public interface IReadOnlyCombatTurnOrderViewModel : IWindowViewModel
    {
        IEnumerable<ICombatTurnOrderPortraitViewModel> Portraits { get; }
    }
}
