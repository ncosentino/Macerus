using System;
using System.ComponentModel;

namespace Macerus.Plugins.Features.CharacterSheet.Api
{
    public interface IWindowViewModel : INotifyPropertyChanged
    {
        event EventHandler<EventArgs> Opened;

        event EventHandler<EventArgs> Closed;

        bool IsOpen { get; set; }

        void Open();

        void Close();
    }
}
