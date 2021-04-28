using Macerus.Plugins.Features.StatusBar.Api;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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

        public IEnumerable<IStatusBarAbilityViewModel> Abilities { get; private set; }

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

        public void UpdateAbilities(IEnumerable<IStatusBarAbilityViewModel> abilities)
        {
            Abilities = abilities.ToArray();

            OnPropertyChanged(nameof(Abilities));
        }
    }
}