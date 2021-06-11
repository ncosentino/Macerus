using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.Inventory.Api.HoverCards;

namespace Macerus.Plugins.Features.Inventory.Default.HoverCards
{
    public sealed class HoverCardViewModel : IHoverCardViewModel
    {
        public HoverCardViewModel(IEnumerable<IHoverCardPartViewModel> parts)
        {
            Parts = parts.ToArray();
        }

        public IReadOnlyCollection<IHoverCardPartViewModel> Parts { get; }
    }
}
