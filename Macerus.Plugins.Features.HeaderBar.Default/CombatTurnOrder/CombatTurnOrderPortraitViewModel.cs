﻿using System;

using Macerus.Plugins.Features.Gui;
using Macerus.Plugins.Features.Gui.Default;
using Macerus.Plugins.Features.HeaderBar.Api.CombatTurnOrder;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.HeaderBar.Default.CombatTurnOrder
{
    public sealed class CombatTurnOrderPortraitViewModel :
        NotifierBase,
        ICombatTurnOrderPortraitViewModel
    {
        public CombatTurnOrderPortraitViewModel(
            IIdentifier actorIdentifier,
            IColor borderColor,
            IColor backgroundColor,
            string actorName,
            IIdentifier iconResourceId)
        {
            ActorIdentifier = actorIdentifier;
            BorderColor = borderColor;
            BackgroundColor = backgroundColor;
            ActorName = actorName;
            IconResourceId = iconResourceId;
        }

        public event EventHandler<EventArgs> Activated;

        public IIdentifier IconResourceId { get; }

        public IIdentifier ActorIdentifier { get; }

        public IColor BorderColor { get; }

        public IColor BackgroundColor { get; }

        public string ActorName { get; }

        public void Activate() => Activated?.Invoke(this, EventArgs.Empty);
    }
}