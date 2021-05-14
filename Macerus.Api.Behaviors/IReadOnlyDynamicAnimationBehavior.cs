using System.Threading.Tasks;

using Macerus.Plugins.Features.Animations.Api;

using ProjectXyz.Api.Framework;

namespace Macerus.Api.Behaviors
{
    public interface IReadOnlyDynamicAnimationBehavior : IReadOnlyAnimationBehavior
    {
        IIdentifier BaseAnimationId { get; }

        ISpriteAnimationFrame CurrentFrame { get; }

        string SourcePattern { get; }

        int CurrentFrameIndex { get; }

        Task<IAnimationMultipliers> GetAnimationMultipliersAsync();
    }
}