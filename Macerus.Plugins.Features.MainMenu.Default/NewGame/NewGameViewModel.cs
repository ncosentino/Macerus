using System;

using Macerus.Plugins.Features.Gui.Default;
using Macerus.Plugins.Features.MainMenu.Api.NewGame;

namespace Macerus.Plugins.Features.MainMenu.Default.NewGame
{
    public sealed class NewGameViewModel : 
        NotifierBase,
        INewGameViewModel
    {
        private bool _isOpen;

        public event EventHandler<EventArgs> RequestNewGame;

        public event EventHandler<EventArgs> RequestGoBack;

        public bool IsOpen
        {
            get { return _isOpen; }
            private set
            {
                if (_isOpen != value)
                {
                    _isOpen = value;
                    OnPropertyChanged();
                }
            }
        }

        public void Open() => IsOpen = true;

        public void Close() => IsOpen = false;

        public void StartNewGame() => RequestNewGame?.Invoke(this, EventArgs.Empty);

        public void GoBack() => RequestGoBack?.Invoke(this, EventArgs.Empty);
    }
}