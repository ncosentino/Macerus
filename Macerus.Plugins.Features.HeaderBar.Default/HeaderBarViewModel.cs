using Macerus.Plugins.Features.Gui.Default;
using Macerus.Plugins.Features.HeaderBar.Api;

namespace Macerus.Plugins.Features.StatusBar.Default
{
    public sealed class HeaderBarViewModel :
        NotifierBase,
        IHeaderBarViewModel
    {
        //public IEnumerable<IStatusBarAbilityViewModel> Abilities { get; private set; }

        //public void UpdateAbilities(IEnumerable<IStatusBarAbilityViewModel> abilities)
        //{
        //    Abilities = abilities.ToArray();

        //    OnPropertyChanged(nameof(Abilities));
        //}
    }
}