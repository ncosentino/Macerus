using ProjectXyz.Api.Framework;

namespace Macerus.Api.Behaviors
{
    public interface IAnimationBehavior : IReadOnlyAnimationBehavior
    {
        new IIdentifier BaseAnimationId { get; set; }

        new bool Visible { get; set; }
    }
}