using System;

namespace Macerus.Plugins.Features.Inventory.Api
{
    public sealed class DragEndEventArgs : EventArgs
    {
        public DragEndEventArgs(bool success)
        {
            Success = success;
        }

        public bool Success { get; }
    }
}