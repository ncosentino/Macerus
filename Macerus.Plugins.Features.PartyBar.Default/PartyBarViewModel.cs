using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.Gui.Default;

namespace Macerus.Plugins.Features.PartyBar.Default
{
    public sealed class PartyBarViewModel :
        NotifierBase,
        IPartyBarViewModel
    {
        private bool _isOpen;

        public PartyBarViewModel()
        {
            Portraits = new IPartyBarPortraitViewModel[0];
        }

        public event EventHandler<EventArgs> Opened;

        public event EventHandler<EventArgs> Closed;

        public IEnumerable<IPartyBarPortraitViewModel> Portraits { get; private set; }

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

        public void UpdatePortraits(IEnumerable<IPartyBarPortraitViewModel> portraits)
        {
            Portraits = portraits.ToArray();
            OnPropertyChanged(nameof(Portraits));
        }
    }
}