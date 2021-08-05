namespace Macerus.Plugins.Features.Inventory.Api.HoverCards
{
    public interface IMultiNameHoverCardPartViewModel : IHoverCardPartViewModel
    {
        string Name { get; }

        string Subname { get; }
    }
}
