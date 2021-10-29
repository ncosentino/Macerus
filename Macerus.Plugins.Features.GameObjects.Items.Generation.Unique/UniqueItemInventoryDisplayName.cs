using System.Diagnostics;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Unique
{
    [DebuggerDisplay("{DisplayName}")]
    public sealed class UniqueItemInventoryDisplayName :
        BaseBehavior,
        IUniqueItemInventoryDisplayName
    {
        public UniqueItemInventoryDisplayName(
            IIdentifier uniqueItemStringResourceId,
            string displayName)
        {
            UniqueItemStringResourceId = uniqueItemStringResourceId;
            DisplayName = displayName;
        }

        public IIdentifier UniqueItemStringResourceId { get; }

        public string DisplayName { get; }
    }
}
