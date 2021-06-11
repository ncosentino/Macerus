using Macerus.Plugins.Features.Inventory.Api.HoverCards;

namespace Macerus.Plugins.Features.Inventory.Default.HoverCards
{
    public sealed class MultiNameHoverCardPartViewModel : IMultiNameHoverCardPartViewModel
    {
        public MultiNameHoverCardPartViewModel(
            string name,
            string subname)
        {
            Name = name;
            Subname = subname;
        }

        public string Name { get; }

        public string Subname { get; }
    }
}
