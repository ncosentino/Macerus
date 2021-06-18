using System;

using Macerus.Plugins.Features.Gui.Default;
using Macerus.Plugins.Features.MainMenu.Api;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.MainMenu.Default
{
    public sealed class MainMenuViewModel :
        NotifierBase,
        IMainMenuViewModel
    {
        private bool _isOpen;

        public event EventHandler<EventArgs> Opened;

        public event EventHandler<EventArgs> Closed;

        public event EventHandler<EventArgs> RequestNewGame;

        public event EventHandler<EventArgs> RequestLoadGame;

        public event EventHandler<EventArgs> RequestOptions;

        public event EventHandler<EventArgs> RequestExit;

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

        public IIdentifier BackgroundImageResourceId { get; } =
            new StringIdentifier("Graphics/Gui/MainMenu/background");

        public void Close() => IsOpen = false;

        public void Open() => IsOpen = true;

        public void StartNewGame() => RequestNewGame?.Invoke(this, EventArgs.Empty);

        public void LoadGame() => RequestLoadGame?.Invoke(this, EventArgs.Empty);

        public void NavigateOptions() => RequestOptions?.Invoke(this, EventArgs.Empty);

        public void ExitGame() => RequestExit?.Invoke(this, EventArgs.Empty);
    }
}