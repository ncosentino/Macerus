using System.ComponentModel;

namespace Macerus.Plugins.Features.Inventory.Api
{
    public interface IItemDragViewModel : INotifyPropertyChanged
    {
        IItemSlotViewModel DraggedItemSlot { get; set; }
    }
}