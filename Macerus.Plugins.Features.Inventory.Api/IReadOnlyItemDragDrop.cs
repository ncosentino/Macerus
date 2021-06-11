using System.ComponentModel;

namespace Macerus.Plugins.Features.Inventory.Api
{
    public interface IReadOnlyItemDragDrop : INotifyPropertyChanged
    {
        bool IsDragOver { get; }

        bool? IsDropAllowed { get; }

        bool IsFocused { get; }
    }
}