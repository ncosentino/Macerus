using System.Collections.Generic;

using Macerus.Plugins.Features.Gui;

namespace Macerus.Plugins.Features.HeaderBar.Api.CombatTurnOrder
{
    public interface IReadOnlyCombatTurnOrderViewModel : IWindowViewModel
    {
        IEnumerable<ICombatTurnOrderPortraitViewModel> Portraits { get; }
    }
}
