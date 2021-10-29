using System.Diagnostics;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation
{
    [DebuggerDisplay("{DisplayName}")]
    public sealed class BaseItemInventoryDisplayName :
        BaseBehavior,
        IBaseItemInventoryDisplayName
    {
        public BaseItemInventoryDisplayName(
            IIdentifier baseItemStringResourceId,
            string displayName)
        {
            BaseItemStringResourceId = baseItemStringResourceId;
            DisplayName = displayName;
        }

        public IIdentifier BaseItemStringResourceId { get; }

        public string DisplayName { get; }
    }
}
