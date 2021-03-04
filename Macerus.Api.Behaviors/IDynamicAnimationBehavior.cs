using ProjectXyz.Api.Framework;

namespace Macerus.Api.Behaviors
{
    public interface IDynamicAnimationBehavior :
        IAnimationBehavior,
        IReadOnlyDynamicAnimationBehavior
    {
        new IIdentifier BaseAnimationId { get; set; }
    }
}