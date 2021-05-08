using System.Collections.Generic;

namespace Macerus.Plugins.Features.HeaderBar.Api.CombatTurnOrder
{
    public interface ICombatTurnOrderViewModel : IReadOnlyCombatTurnOrderViewModel
    {
        void UpdatePortraits(IEnumerable<ICombatTurnOrderPortraitViewModel> portraits);
    }
}
