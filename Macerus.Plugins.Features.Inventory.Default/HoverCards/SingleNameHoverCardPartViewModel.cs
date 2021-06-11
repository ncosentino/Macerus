using Macerus.Plugins.Features.Inventory.Api.HoverCards;

namespace Macerus.Plugins.Features.Inventory.Default.HoverCards
{
    public sealed class SingleNameHoverCardPartViewModel : ISingleNameHoverCardPartViewModel
    {
        public SingleNameHoverCardPartViewModel(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
