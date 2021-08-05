namespace Macerus.Plugins.Features.Inventory.Api.HoverCards
{
    public interface IHoverCardPartConverterLoadOrder
    {
        int GetOrder(IBehaviorsToHoverCardPartViewModelConverter converter);
    }
}
