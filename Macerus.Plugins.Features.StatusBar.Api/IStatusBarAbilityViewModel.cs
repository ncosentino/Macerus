using Macerus.Plugins.Features.Gui.Api;
using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.StatusBar.Api
{
    public interface IStatusBarAbilityViewModel
    {
        float IconOpacity { get; }

        IColor IconColor { get; }

        IIdentifier IconResourceId { get; }

        string AbilityName { get; }
    }
}
