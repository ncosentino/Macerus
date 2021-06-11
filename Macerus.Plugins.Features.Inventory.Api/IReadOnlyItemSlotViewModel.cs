
using System;

using Macerus.Plugins.Features.Gui.Api;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Inventory.Api
{
    public interface IReadOnlyItemSlotViewModel : IReadOnlyItemDragDrop
    {
        event EventHandler<PopulateHoverCardFromSlotEventArgs> RequestPopulateHoverCardContent;

        object Id { get; }

        bool HasItem { get; }

        IIdentifier IconResourceId { get; }

        float IconOpacity { get; }

        IColor IconColor { get; }

        IColor SlotBackgroundColor { get; }

        string SlotLabel { get; }

        bool ShowLabel { get; }
    }
}