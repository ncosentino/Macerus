using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Macerus.Plugins.Features.CharacterSheet.Api;
using Macerus.Plugins.Features.Gui.Default;

namespace Macerus.Plugins.Features.CharacterSheet.Default
{
    public sealed class CharacterSheetViewModel :
        NotifierBase,
        ICharacterSheetViewModel
    {
        private bool _isOpen;
        private readonly ObservableCollection<ICharacterStatViewModel> _stats;

        public CharacterSheetViewModel()
        {
            _isOpen = false;
            _stats = new ObservableCollection<ICharacterStatViewModel>();
        }

        public event EventHandler<EventArgs> Opened;

        public event EventHandler<EventArgs> Closed;

        public bool IsOpen
        {
            get { return _isOpen; }
            set
            {
                if (_isOpen != value)
                {
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
        }

        public IEnumerable<ICharacterStatViewModel> Stats => _stats;

        public void Close() => IsOpen = false;

        public void Open() => IsOpen = true;

        public void UpdateStats(IEnumerable<ICharacterStatViewModel> statViewModels)
        {
            _stats.Clear();
            
            foreach (var stat in statViewModels)
            {
                _stats.Add(stat);
            }

            OnPropertyChanged(nameof(Stats));
        }
    }
}