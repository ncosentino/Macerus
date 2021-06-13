using System;

using Macerus.Plugins.Features.Gui.Default;
using Macerus.Plugins.Features.InGameMenu.Api;

namespace Macerus.Plugins.Features.InGameMenu.Default
{
    public sealed class InGameMenuViewModel :
        NotifierBase,
        IInGameMenuViewModel
    {
        private bool _isOpen;

        public event EventHandler<EventArgs> Opened;

        public event EventHandler<EventArgs> Closed;

        public event EventHandler<EventArgs> RequestGoToMainMenu;

        public event EventHandler<EventArgs> RequestOptions;

        public event EventHandler<EventArgs> RequestExit;

        public event EventHandler<EventArgs> RequestClose;

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

        public void GoToMainMenu() => RequestGoToMainMenu?.Invoke(this, EventArgs.Empty);

        public void GoToOptions() => RequestOptions?.Invoke(this, EventArgs.Empty);

        public void ExitGame() => RequestExit?.Invoke(this, EventArgs.Empty);

        public void CloseMenu() => RequestClose?.Invoke(this, EventArgs.Empty);
    }
}