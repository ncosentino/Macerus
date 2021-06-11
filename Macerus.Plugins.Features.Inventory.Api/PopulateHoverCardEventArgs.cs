using System;

namespace Macerus.Plugins.Features.Inventory.Api
{
    public sealed class PopulateHoverCardFromSlotEventArgs : EventArgs
    {
        public PopulateHoverCardFromSlotEventArgs(
            IItemSlotViewModel itemSlotViewModel,
            object hoverCardContent)
        {
            ItemSlotViewModel = itemSlotViewModel;
            HoverCardContent = hoverCardContent;
        }

        public IItemSlotViewModel ItemSlotViewModel { get; }

        public object HoverCardContent { get; } 
    }
}