﻿using System;

using Macerus.Plugins.Features.Gui.Api;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.MainMenu.Api
{
    public interface IMainMenuViewModel : IWindowViewModel
    {
        event EventHandler<EventArgs> RequestNewGame;

        event EventHandler<EventArgs> RequestOptions;

        event EventHandler<EventArgs> RequestExit;

        IIdentifier BackgroundImageResourceId { get; }

        void StartNewGame();

        void NavigateOptions();

        void ExitGame();
    }
}