using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.GameObjects.Skills.Api;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.GameObjects.Skills;

namespace Macerus.Plugins.Features.GameObjects.Skills.Default
{
    public sealed class AnimateSkillUserSkillHandler : IDiscoverableSkillHandler
    {
        public int? Priority { get; } = int.MinValue;

        public void Handle(
            IGameObject user,
            IGameObject skill)
        {
            if (!user.TryGetFirst<IDynamicAnimationBehavior>(out var animationBehavior))
            {
                return;
            }

            if (!skill.TryGetFirst<IActorAnimationOnUseBehavior>(out var actorAnimationOnUseBehavior))
            {
                return;
            }

            animationBehavior.BaseAnimationId = actorAnimationOnUseBehavior.AnimationId;
        }
    }
}
