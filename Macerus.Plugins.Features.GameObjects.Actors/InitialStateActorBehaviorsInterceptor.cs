using System.Collections.Generic;

using Macerus.Api.Behaviors;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Stats;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;

namespace Macerus.Plugins.Features.GameObjects.Actors
{
    public sealed class InitialStateActorBehaviorsInterceptor : IDiscoverableActorBehaviorsInterceptor
    {
        private readonly IReadOnlyStatDefinitionToTermMappingRepositoryFacade _statDefinitionToTermMappingRepository;
        private readonly IActorIdentifiers _actorIdentifiers;

        public InitialStateActorBehaviorsInterceptor(
            IReadOnlyStatDefinitionToTermMappingRepositoryFacade statDefinitionToTermMappingRepository,
            IActorIdentifiers actorIdentifiers)
        {
            _statDefinitionToTermMappingRepository = statDefinitionToTermMappingRepository;
            _actorIdentifiers = actorIdentifiers;
        }

        public int Priority => -10000;

        IEnumerable<IBehavior> IActorBehaviorsInterceptor.Intercept(IReadOnlyCollection<IBehavior> behaviors)
        {
            var dynamicAnimationBehavior = behaviors.GetOnly<IDynamicAnimationBehavior>();
            dynamicAnimationBehavior.BaseAnimationId = _actorIdentifiers.AnimationStandForward;
            return behaviors;
        }
    }
}
