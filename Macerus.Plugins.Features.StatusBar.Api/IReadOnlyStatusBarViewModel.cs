using System.Collections.Generic;
using System.ComponentModel;

namespace Macerus.Plugins.Features.StatusBar.Api
{
    public interface IReadOnlyStatusBarViewModel : INotifyPropertyChanged
    {
        IStatusBarStringProvider StringProvider { get; }

        bool CanCompleteTurn { get; }

        bool IsOpen { get; }

        IStatusBarResourceViewModel LeftResource { get; }

        IStatusBarResourceViewModel RightResource { get; }

        IReadOnlyCollection<IStatusBarAbilityViewModel> Abilities { get; }
    }
}
