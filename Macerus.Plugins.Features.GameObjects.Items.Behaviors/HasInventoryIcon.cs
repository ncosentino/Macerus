using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Items.Behaviors
{
    public sealed class HasInventoryIcon :
        BaseBehavior,
        IHasInventoryIcon
    {
        public HasInventoryIcon(string iconResource)
        {
            IconResource = iconResource;
        }

        public string IconResource { get; }
    }
}
