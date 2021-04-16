using System.ComponentModel;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Inventory.Api
{
    public interface IItemSlotViewModel : INotifyPropertyChanged
    {
        object Id { get; }

        bool HasItem { get; }

        IIdentifier IconResourceId { get; }

        string SlotLabel { get; }

        bool ShowLabel { get; }

        bool IsDragOver { get; set; }

        bool? IsDropAllowed { get; set; }

        bool IsFocused { get; set; }
    }
}