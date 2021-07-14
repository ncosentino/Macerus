using System.Threading.Tasks;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.GameObjects.Actors.Triggers;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;

namespace Macerus.Plugins.Features.GameObjects.Actors.Default.Triggers.Death
{
    public sealed class DeathAnimationTriggerMechanic : IDeathTriggerMechanic
    {
        private readonly IActorIdentifiers _actorIdentifiers;

        public DeathAnimationTriggerMechanic(IActorIdentifiers actorIdentifiers)
        {
            _actorIdentifiers = actorIdentifiers;
        }

        public async Task ActorDeathTriggeredAsync(IGameObject actor)
        {
            var dynamicAnimationBehavior = actor.GetOnly<IDynamicAnimationBehavior>();
            dynamicAnimationBehavior.BaseAnimationId = _actorIdentifiers.AnimationDeath;
        }
    }
}
