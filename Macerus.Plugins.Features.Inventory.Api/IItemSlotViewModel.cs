using System.ComponentModel;

using Macerus.Plugins.Features.Gui.Api;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Inventory.Api
{
    public interface IItemSlotViewModel : INotifyPropertyChanged
    {
        object Id { get; }

        bool HasItem { get; }

        IIdentifier IconResourceId { get; }

        float IconOpacity { get; }

        IColor IconColor { get; }

        IColor SlotBackgroundColor { get; }

        string SlotLabel { get; }

        bool ShowLabel { get; }

        bool IsDragOver { get; set; }

        bool? IsDropAllowed { get; set; }

        bool IsFocused { get; set; }
    }
}