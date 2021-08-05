using System;
using System.Collections.Generic;

namespace Macerus.Plugins.Features.Inventory.Api.HoverCards
{
    public interface IBaseStatsHoverCardPartViewModel : IHoverCardPartViewModel
    {
        IReadOnlyCollection<Tuple<string, double>> NamesAndValues { get; }
    }
}
