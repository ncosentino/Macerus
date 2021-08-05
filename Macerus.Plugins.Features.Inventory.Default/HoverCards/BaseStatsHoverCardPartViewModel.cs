using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.Inventory.Api.HoverCards;

namespace Macerus.Plugins.Features.Inventory.Default.HoverCards
{
    public sealed class BaseStatsHoverCardPartViewModel : IBaseStatsHoverCardPartViewModel
    {
        public BaseStatsHoverCardPartViewModel(IEnumerable<Tuple<string, double>> namesAndValues)
        {
            NamesAndValues = namesAndValues.ToArray();
        }

        public IReadOnlyCollection<Tuple<string, double>> NamesAndValues { get; }
    }
}
