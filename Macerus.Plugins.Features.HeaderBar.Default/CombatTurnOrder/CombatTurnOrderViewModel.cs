﻿using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.Gui.Default;
using Macerus.Plugins.Features.HeaderBar.Api.CombatTurnOrder;

namespace Macerus.Plugins.Features.HeaderBar.Default.CombatTurnOrder
{
    public sealed class CombatTurnOrderViewModel :
        NotifierBase,
        ICombatTurnOrderViewModel
    {
        private bool _isOpen;

        public CombatTurnOrderViewModel()
        {
            Portraits = new ICombatTurnOrderPortraitViewModel[0];
        }

        public event EventHandler<EventArgs> Opened;

        public event EventHandler<EventArgs> Closed;

        public IEnumerable<ICombatTurnOrderPortraitViewModel> Portraits { get; private set; }

        public bool IsOpen
        {
            get => _isOpen;
            set
            {
                if (_isOpen == value)
                {
                    return;
                }

                _isOpen = value;
                OnPropertyChanged();

                if (_isOpen)
                {
                    Opened?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    Closed?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public void Close() => IsOpen = false;

        public void Open() => IsOpen = true;

        public void UpdatePortraits(IEnumerable<ICombatTurnOrderPortraitViewModel> portraits)
        {
            Portraits = portraits.ToArray();
            OnPropertyChanged(nameof(Portraits));
        }
    }
}