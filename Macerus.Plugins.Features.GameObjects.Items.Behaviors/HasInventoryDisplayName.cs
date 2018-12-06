using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Items.Behaviors
{
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
