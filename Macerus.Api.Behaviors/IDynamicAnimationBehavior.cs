using System.Threading.Tasks;

using ProjectXyz.Api.Framework;

namespace Macerus.Api.Behaviors
{
    public interface IDynamicAnimationBehavior :
        IAnimationBehavior,
        IObservableDynamicAnimationBehavior
    {
        new IIdentifier BaseAnimationId { get; set; }

        Task UpdateAnimationAsync(double secondsSinceLastFrame);
    }
}