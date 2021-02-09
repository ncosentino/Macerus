using ProjectXyz.Api.Framework;

namespace Macerus.Api.Behaviors
{
    public interface IAnimationBehavior : IReadOnlyAnimationBehavior
    {
        new IIdentifier CurrentAnimationId { get; set; }
    }
}