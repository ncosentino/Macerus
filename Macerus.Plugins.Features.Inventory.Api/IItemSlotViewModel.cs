namespace Macerus.Plugins.Features.Inventory.Api
{
    public interface IItemSlotViewModel :
        IReadOnlyItemSlotViewModel,
        IItemDragDrop
    {
        void PopulateHoverCard(object hoverCardContainerView);
    }
}