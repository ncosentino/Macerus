using System.Threading.Tasks;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.GameObjects.Skills;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.GameObjects.Skills;

namespace Macerus.Plugins.Features.GameObjects.Skills.Default
{
    public sealed class AnimateSkillUserSkillEffectHandler : IDiscoverableSkillEffectHandler
    {
        public int? Priority { get; } = int.MinValue;

        public async Task HandleAsync(
            IGameObject user,
            IGameObject skillEffect)
        {
            if (!user.TryGetFirst<IDynamicAnimationBehavior>(out var animationBehavior))
            {
                return;
            }

            if (!skillEffect.TryGetFirst<IActorAnimationOnUseBehavior>(out var actorAnimationOnUseBehavior))
            {
                return;
            }

            animationBehavior.BaseAnimationId = actorAnimationOnUseBehavior.AnimationId;
        }
    }
}
