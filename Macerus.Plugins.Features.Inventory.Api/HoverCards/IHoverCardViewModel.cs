using System.Collections.Generic;

namespace Macerus.Plugins.Features.Inventory.Api.HoverCards
{
    public interface IHoverCardViewModel
    {
        IReadOnlyCollection<IHoverCardPartViewModel> Parts { get; }
    }
}
