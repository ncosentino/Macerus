using System;
using System.ComponentModel;

using Macerus.Plugins.Features.Gui;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.HeaderBar.Api.CombatTurnOrder
{
    public interface ICombatTurnOrderPortraitViewModel : INotifyPropertyChanged
    {
        event EventHandler<EventArgs> Activated;

        IIdentifier IconResourceId { get; }

        IIdentifier ActorIdentifier { get; }

        IColor BorderColor { get; }

        IColor BackgroundColor { get; }

        string ActorName { get; }

        void Activate();
    }
}
