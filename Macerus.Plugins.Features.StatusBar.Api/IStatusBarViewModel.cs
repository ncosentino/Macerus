using System.ComponentModel;

namespace Macerus.Plugins.Features.StatusBar.Api
{
    public interface IStatusBarViewModel : INotifyPropertyChanged
    {
        IStatusBarResourceViewModel LeftResource { get; }

        IStatusBarResourceViewModel RightResource { get; }

        void UpdateResource(IStatusBarResourceViewModel resource, bool left);
    }
}
