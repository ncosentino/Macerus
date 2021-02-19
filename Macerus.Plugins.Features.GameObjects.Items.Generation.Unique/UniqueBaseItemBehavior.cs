using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Unique
{
    public sealed class UniqueBaseItemBehavior :
        BaseBehavior,
        IBehavior
    {
        public UniqueBaseItemBehavior(IIdentifier baseItemId)
        {
            BaseItemId = baseItemId;
        }

        public IIdentifier BaseItemId { get; }
    }
}
