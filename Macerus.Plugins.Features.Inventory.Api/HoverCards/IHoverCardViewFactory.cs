namespace Macerus.Plugins.Features.Inventory.Api.HoverCards
{
    public interface IHoverCardViewFactory
    {
        object Create(IHoverCardViewModel viewModel);
    }

    public interface IHoverCardPartViewConverterFacade : IHoverCardPartViewConverter
    {
    }

    public interface IDiscoverableHoverCardPartViewConverter : IHoverCardPartViewConverter
    {
        bool CanHandle(IHoverCardPartViewModel viewModel);
    }

    public interface IHoverCardPartViewConverter
    {
        object Create(IHoverCardPartViewModel viewModel);
    }
}
