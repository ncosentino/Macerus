using System;

using Macerus.Plugins.Features.Gui.Api;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.InGameMenu.Api
{
    public interface IInGameMenuViewModel : IWindowViewModel
    {
        event EventHandler<EventArgs> RequestGoToMainMenu;

        event EventHandler<EventArgs> RequestOptions;

        event EventHandler<EventArgs> RequestClose;

        event EventHandler<EventArgs> RequestExit;

        void GoToMainMenu();

        void GoToOptions();

        void CloseMenu();

        void ExitGame();
    }
}
