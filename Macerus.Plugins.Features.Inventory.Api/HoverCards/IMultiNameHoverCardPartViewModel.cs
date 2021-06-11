namespace Macerus.Plugins.Features.Inventory.Api.HoverCards
{
    public interface IMultiNameHoverCardPartViewModel : IHoverCardPartViewModel
    {
        string Name { get; }

        string Subname { get; }
    }

    //public sealed class HoverCardViewFactory : IHoverCardViewFactory
    //{
    //    public IHoverCardView Create(IHoverCardViewModel viewModel)
    //    {

    //    }
    //}
}
