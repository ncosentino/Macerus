namespace Macerus.Plugins.Features.Inventory.Api
{
    public interface IItemDragDrop : IReadOnlyItemDragDrop
    {
        new bool IsDragOver { get; set; }

        new bool? IsDropAllowed { get; set; }

        new bool IsFocused { get; set; }
    }
}