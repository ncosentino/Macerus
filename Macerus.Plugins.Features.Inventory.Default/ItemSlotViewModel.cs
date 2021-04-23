using Macerus.Plugins.Features.Gui.Api;
using Macerus.Plugins.Features.Gui.Default;
using Macerus.Plugins.Features.Inventory.Api;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Inventory.Default
{
    public sealed class ItemSlotViewModel :
        NotifierBase,
        IItemSlotViewModel
    {
        private bool _isDragOver;
        private bool? _isDropAllowed;
        private bool _isFocused;

        public ItemSlotViewModel(
            object id,
            bool hasItem,
            IIdentifier iconResourceId,
            float iconOpacity,
            IColor iconColor,
            IColor slotBackgroundColor,
            string slotLabel)
        {
            Id = id;
            HasItem = hasItem;
            IconResourceId = iconResourceId;
            IconOpacity = iconOpacity;
            IconColor = iconColor;
            SlotBackgroundColor = slotBackgroundColor;
            SlotLabel = slotLabel;
            ShowLabel = !string.IsNullOrEmpty(slotLabel);
        }

        public object Id { get; }

        public bool HasItem { get; }

        public IIdentifier IconResourceId { get; }

        public float IconOpacity { get; }

        public IColor IconColor { get; }

        public IColor SlotBackgroundColor { get; }

        public string SlotLabel { get; }

        public bool ShowLabel { get; }

        public bool IsDragOver
        {
            get { return _isDragOver; }
            set
            {
                if (_isDragOver != value)
                {
                    _isDragOver = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool? IsDropAllowed
        {
            get { return _isDropAllowed; }
            set
            {
                if (_isDropAllowed != value)
                {
                    _isDropAllowed = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsFocused
        {
            get { return _isFocused; }
            set
            {
                if (_isFocused != value)
                {
                    _isFocused = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}