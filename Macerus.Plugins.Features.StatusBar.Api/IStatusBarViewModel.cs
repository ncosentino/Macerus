using System.Collections.Generic;

namespace Macerus.Plugins.Features.StatusBar.Api
{
    public interface IStatusBarViewModel : IObservableStatusBarViewModel
    {
        new bool CanCompleteTurn { get; set; }

        new bool IsOpen { get; set; }

        void UpdateResource(
            IStatusBarResourceViewModel resource,
            bool left);

        void UpdateAbilities(
            IReadOnlyCollection<IStatusBarAbilityViewModel> abilities);

        void CompleteTurn();
    }
}
