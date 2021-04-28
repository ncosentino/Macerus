using Macerus.Plugins.Features.StatusBar.Api;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.StatusBar.Default
{
    public sealed class StatusBarAbilityViewModel : IStatusBarAbilityViewModel
    {
        public StatusBarAbilityViewModel(
            bool isEnabled,
            IIdentifier identifier,
            string abilityName)
        {
            IsEnabled = isEnabled;
            IconResourceId = identifier;
            AbilityName = abilityName;
        }

        public bool IsEnabled { get; }

        public string AbilityName { get; }

        public IIdentifier IconResourceId { get; }
    }
}
