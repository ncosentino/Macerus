using Macerus.Plugins.Features.Inventory.Api;

namespace Macerus.Plugins.Features.Inventory.Default
{
    public sealed class ItemDragViewModel :
        NotifierBase,
        IItemDragViewModel
    {
        private IItemSlotViewModel _draggedItem;

        public IItemSlotViewModel DraggedItemSlot
        {
            get => _draggedItem;
            set
            {
                if (_draggedItem != value)
                {
                    _draggedItem = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}