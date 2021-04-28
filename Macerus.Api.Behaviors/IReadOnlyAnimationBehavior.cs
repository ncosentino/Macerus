using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;

namespace Macerus.Api.Behaviors
{
    public interface IReadOnlyAnimationBehavior : IBehavior
    {
        IIdentifier CurrentAnimationId { get; }

        bool Visible { get; }
    }
}