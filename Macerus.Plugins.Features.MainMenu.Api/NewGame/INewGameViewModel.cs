using System;
using System.ComponentModel;

namespace Macerus.Plugins.Features.MainMenu.Api.NewGame
{
    public interface INewGameViewModel : INotifyPropertyChanged
    {
        bool IsOpen { get; }

        event EventHandler<EventArgs> RequestGoBack;

        event EventHandler<EventArgs> RequestNewGame;

        void Close();

        void GoBack();

        void Open();

        void StartNewGame();
    }
}