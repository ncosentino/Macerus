using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<IStatusBarAbilityViewModel> Abilities { get; private set; }

        public void UpdateResource(IStatusBarResourceViewModel resource, bool left)
        {
            if (left)
            {
                if (LeftResource?.Current != resource.Current ||
                    LeftResource?.Maximum != resource.Maximum ||
                    LeftResource?.Name != resource.Name)
                {
                    LeftResource = resource;
                    OnPropertyChanged(nameof(LeftResource));
                }
            }
            else
            {
                if (RightResource?.Current != resource.Current ||
                     RightResource?.Maximum != resource.Maximum ||
                     RightResource?.Name != resource.Name)
                {
                    RightResource = resource;
                    OnPropertyChanged(nameof(RightResource));
                }
            }
        }

        public void UpdateAbilities(IEnumerable<IStatusBarAbilityViewModel> abilities)
        {
            Abilities = abilities.ToArray();

            OnPropertyChanged(nameof(Abilities));
        }
    }
}