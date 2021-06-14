using Macerus.Plugins.Features.Gui.Default;
using Macerus.Plugins.Features.LoadingScreen.Api;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.MainMenu.Default
{
    public sealed class LoadingScreenViewModel :
        NotifierBase,
        ILoadingScreenViewModel
    {
        private IIdentifier _backgroundImageResourceId;

        public IIdentifier BackgroundImageResourceId
        {
            get { return _backgroundImageResourceId; }
            set
            {
                if (!Equals(_backgroundImageResourceId, value))
                {
                    _backgroundImageResourceId = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}