using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Items.Behaviors
{
    public sealed class HasInventoryIcon :
        BaseBehavior,
        IHasInventoryIcon
    {
        public HasInventoryIcon(IIdentifier iconResourceId)
        {
            IconResourceId = iconResourceId;
        }

        public IIdentifier IconResourceId { get; }
    }
}
