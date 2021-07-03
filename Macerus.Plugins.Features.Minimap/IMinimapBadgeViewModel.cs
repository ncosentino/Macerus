using System;
using System.ComponentModel;

namespace Macerus.Plugins.Features.Minimap
{
    public interface IMinimapBadgeViewModel : INotifyPropertyChanged
    {
        event EventHandler<EventArgs> Opened;

        event EventHandler<EventArgs> Closed;

        bool IsOpen { get; set; }

        void Open();

        void Close();
    }
}
