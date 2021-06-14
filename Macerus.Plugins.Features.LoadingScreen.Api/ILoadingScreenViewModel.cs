using System.ComponentModel;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.LoadingScreen.Api
{
    public interface ILoadingScreenViewModel : INotifyPropertyChanged
    {
        IIdentifier BackgroundImageResourceId { get; set; }
    }
}
