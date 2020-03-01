using System.Diagnostics;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Items.Behaviors
{
    [DebuggerDisplay("{DisplayName}")]
    public sealed class HasInventoryDisplayName :
        BaseBehavior,
        IHasInventoryDisplayName
    {
        public HasInventoryDisplayName(string displayName)
        {
            DisplayName = displayName;
        }

        public string DisplayName { get; }
    }
}
