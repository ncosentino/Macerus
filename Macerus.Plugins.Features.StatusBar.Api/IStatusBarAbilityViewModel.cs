using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.StatusBar.Api
{
    public interface IStatusBarAbilityViewModel
    {
        bool IsEnabled { get; }

        string AbilityName { get; }

        IIdentifier IconResourceId { get; }
    }
}
