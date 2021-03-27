using System.Threading.Tasks;

using ProjectXyz.Api.Framework;

namespace Macerus.Api.Behaviors
{
    public interface IReadOnlyDynamicAnimationBehavior : IReadOnlyAnimationBehavior
    {
        IIdentifier BaseAnimationId { get; }
    }
}