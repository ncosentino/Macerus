using System.Collections.Generic;

namespace Macerus.Plugins.Features.PartyBar
{
    public interface IPartyBarViewModel : IReadOnlyPartyBarViewModel
    {
        void UpdatePortraits(IEnumerable<IPartyBarPortraitViewModel> portraits);
    }
}
