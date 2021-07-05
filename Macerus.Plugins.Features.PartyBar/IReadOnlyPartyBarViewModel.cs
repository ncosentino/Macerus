using System.Collections.Generic;

using Macerus.Plugins.Features.Gui;

namespace Macerus.Plugins.Features.PartyBar
{
    public interface IReadOnlyPartyBarViewModel : IWindowViewModel
    {
        IEnumerable<IPartyBarPortraitViewModel> Portraits { get; }
    }
}
