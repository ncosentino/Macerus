using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Framework;

namespace Macerus.Api.Behaviors
{
    public interface IReadOnlyAnimationBehavior : IBehavior
    {
        IIdentifier BaseAnimationId { get; }

        bool Visible { get; }
    }
}