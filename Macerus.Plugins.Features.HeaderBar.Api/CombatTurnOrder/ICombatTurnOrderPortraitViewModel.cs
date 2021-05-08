using System.ComponentModel;

using Macerus.Plugins.Features.Gui.Api;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.HeaderBar.Api.CombatTurnOrder
{
    public interface ICombatTurnOrderPortraitViewModel : INotifyPropertyChanged
    {
        IIdentifier IconResourceId { get; }

        IIdentifier ActorIdentifier { get; }

        IColor BorderColor { get; }

        IColor BackgroundColor { get; }

        string ActorName { get; }
    }
}
