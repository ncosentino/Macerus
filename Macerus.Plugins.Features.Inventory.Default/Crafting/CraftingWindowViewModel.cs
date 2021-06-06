using System;

using Macerus.Plugins.Features.Gui.Default;
using Macerus.Plugins.Features.Inventory.Api.Crafting;

namespace Macerus.Plugins.Features.Inventory.Default.Crafting
{
    public sealed class CraftingWindowViewModel :
        NotifierBase,
        ICraftingWindowViewModel
    {
        private bool _isOpen;

        public event EventHandler<EventArgs> Opened;

        public event EventHandler<EventArgs> Closed;

        public bool IsOpen
        {
            get { return _isOpen; }
            set
            {
                if (_isOpen != value)
                {
                    _isOpen = value;
                    OnPropertyChanged();

                    if (_isOpen)
                    {
                        Opened?.Invoke(this, EventArgs.Empty);
                    }
                    else
                    {
                        Closed?.Invoke(this, EventArgs.Empty);
                    }
                }
            }
        }

        public void Close() => IsOpen = false;

        public void Open() => IsOpen = true;
    }
}