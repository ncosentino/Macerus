using System.Linq;

namespace Macerus.Plugins.Features.Hud
{
    public static class IHudViewModelExtensionMethods
    {
        public static bool AnyWindowsOpen(this IReadOnlyHudViewModel hudViewModel)
        {
            var anyOpen = hudViewModel.Windows.Any(x => x.IsOpen);
            return anyOpen;
        }
    }
}
