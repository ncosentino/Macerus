using Macerus.Plugins.Features.StatusBar.Api;

namespace Macerus.Plugins.Features.StatusBar.Default
{
    public sealed class StatusBarViewModel :
        NotifierBase,
        IStatusBarViewModel
    {
        public StatusBarViewModel()
        {
        }

        public IStatusBarResourceViewModel LeftResource { get; private set; }

        public IStatusBarResourceViewModel RightResource { get; private set; }

        public void UpdateResource(IStatusBarResourceViewModel resource, bool left)
        {
            if (left)
            {
                LeftResource = resource;
                OnPropertyChanged(nameof(LeftResource));
            }
            else
            {
                RightResource = resource;
                OnPropertyChanged(nameof(RightResource));
            }
        }
    }
}