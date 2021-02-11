using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Stats;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;

namespace Macerus.Plugins.Features.GameObjects.Actors
{
    public sealed class ActorBehaviorsInterceptor : IActorBehaviorsInterceptor
    {
        private readonly IReadOnlyStatDefinitionToTermMappingRepository _statDefinitionToTermMappingRepository;

        public ActorBehaviorsInterceptor(IReadOnlyStatDefinitionToTermMappingRepository statDefinitionToTermMappingRepository)
        {
            _statDefinitionToTermMappingRepository = statDefinitionToTermMappingRepository;
        }

        public void Intercept(ICollection<IBehavior> behaviors)
        {
            var mutableStats = behaviors.GetOnly<IHasMutableStatsBehavior>();
            mutableStats.MutateStats(stats =>
            {
                stats[_statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("LIGHT_RADIUS_RADIUS").StatDefinitionId] = 10;
                stats[_statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("LIGHT_RADIUS_INTENSITY").StatDefinitionId] = 0.5;
                stats[_statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("LIGHT_RADIUS_RED").StatDefinitionId] = 1;
                stats[_statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("LIGHT_RADIUS_GREEN").StatDefinitionId] = 0;
                stats[_statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("LIGHT_RADIUS_BLUE").StatDefinitionId] = 1;
            });
        }
    }
}
