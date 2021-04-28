using System.Collections.Generic;
using System.ComponentModel;

namespace Macerus.Plugins.Features.StatusBar.Api
{
    public interface IStatusBarViewModel : INotifyPropertyChanged
    {
        IStatusBarResourceViewModel LeftResource { get; }

        IStatusBarResourceViewModel RightResource { get; }

        IEnumerable<IStatusBarAbilityViewModel> Abilities { get; }

        void UpdateResource(IStatusBarResourceViewModel resource, bool left);

        void UpdateAbilities(
            IEnumerable<IStatusBarAbilityViewModel> abilities);
    }
}
