using System;

using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.Inventory.Api
{
    public sealed class PopulateHoverCardFromItemEventArgs : EventArgs
    {
        public PopulateHoverCardFromItemEventArgs(
            IGameObject item,
            object hoverCardContent)
        {
            Item = item;
            HoverCardContent = hoverCardContent;
        }

        public IGameObject Item { get; }

        public object HoverCardContent { get; }
    }
}