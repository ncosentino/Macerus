using Macerus.Plugins.Features.Gui.Api;
using Macerus.Plugins.Features.StatusBar.Api;
using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.StatusBar.Default
{
    public sealed class StatusBarAbilityViewModel : IStatusBarAbilityViewModel
    {
        public StatusBarAbilityViewModel(
            float iconOpacity,
            IColor iconColor,
            IIdentifier identifier,
            string abilityName)
        {
            IconOpacity = iconOpacity;
            IconColor = iconColor;
            IconResourceId = identifier;
            AbilityName = abilityName;
        }

        public float IconOpacity { get; }

        public IColor IconColor { get; }

        public IIdentifier IconResourceId { get; }

        public string AbilityName { get; }
    }
}
