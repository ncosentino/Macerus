using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.Gui.Default;
using Macerus.Plugins.Features.StatusBar.Api;

namespace Macerus.Plugins.Features.StatusBar.Default
{
    public sealed class StatusBarViewModel :
        NotifierBase,
        IStatusBarViewModel
    {
        private readonly List<IStatusBarAbilityViewModel> _abilityViewModels;

        public StatusBarViewModel()
        {
            _abilityViewModels = new List<IStatusBarAbilityViewModel>();
        }

        public IStatusBarResourceViewModel LeftResource { get; private set; }

        public IStatusBarResourceViewModel RightResource { get; private set; }

        public IReadOnlyCollection<IStatusBarAbilityViewModel> Abilities => _abilityViewModels;

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

        public void UpdateAbilities(IReadOnlyCollection<IStatusBarAbilityViewModel> abilities)
        {
            var hasChanged = abilities.Count != _abilityViewModels.Count;

            if (!hasChanged)
            {
                using (var newEnumerator = abilities.GetEnumerator())
                using (var existingEnumerator = _abilityViewModels.GetEnumerator())
                {
                    while (newEnumerator.MoveNext() && existingEnumerator.MoveNext())
                    {
                        var existingViewModel = existingEnumerator.Current;
                        var newViewModel = newEnumerator.Current;

                        if (!string.Equals(existingViewModel.AbilityName, newViewModel.AbilityName))
                        {
                            hasChanged = true;
                            break;
                        }

                        if (existingViewModel.IsEnabled != newViewModel.IsEnabled)
                        {
                            hasChanged = true;
                            break;
                        }

                        if (!Equals(existingViewModel.IconResourceId, newViewModel.IconResourceId))
                        {
                            hasChanged = true;
                            break;
                        }
                    }
                }
            }

            if (hasChanged)
            {
                _abilityViewModels.Clear();
                _abilityViewModels.AddRange(abilities);
                OnPropertyChanged(nameof(Abilities));
                return;
            }
        }
    }
}