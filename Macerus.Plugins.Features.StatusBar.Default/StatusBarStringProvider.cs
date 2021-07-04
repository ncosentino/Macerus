using Macerus.Plugins.Features.StatusBar.Api;

namespace Macerus.Plugins.Features.StatusBar.Default
{
    public sealed class StatusBarStringProvider : IStatusBarStringProvider
    {
        public string CompleteTurnButtonLabel { get; } = "Complete Turn";
    }
}