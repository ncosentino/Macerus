using System.Threading.Tasks;

using Macerus.Plugins.Features.Animations.Api;

using ProjectXyz.Api.Framework;

namespace Macerus.Api.Behaviors
{
    public interface IReadOnlyDynamicAnimationBehavior : IReadOnlyAnimationBehavior
    {
        IIdentifier BaseAnimationId { get; }

        ISpriteAnimationFrame CurrentFrame { get; }

        Task<IAnimationMultipliers> GetAnimationMultipliersAsync();
    }
}