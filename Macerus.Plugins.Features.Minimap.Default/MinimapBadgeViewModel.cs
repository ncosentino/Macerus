using System;

using Macerus.Plugins.Features.Gui.Default;

namespace Macerus.Plugins.Features.Minimap.Default
{
    public sealed class MinimapBadgeViewModel :
        NotifierBase,
        IMinimapBadgeViewModel
    {
        private bool _isOpen;

        public event EventHandler<EventArgs> Opened;

        public event EventHandler<EventArgs> Closed;

        public bool IsOpen
        {
            get => _isOpen;
            set
            {
                if (_isOpen == value)
                {
                    return;
                }

                _isOpen = value;
                if (value)
                {
                    Opened?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    Closed?.Invoke(this, EventArgs.Empty);
                }

                OnPropertyChanged();
            }
        }

        public void Close() => IsOpen = false;

        public void Open() => IsOpen = true;
    }
}
