using System.Collections.Generic;

using Macerus.Plugins.Features.GameObjects.Actors.Api;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Stats;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;

namespace Macerus.Plugins.Features.GameObjects.Actors
{
    public sealed class ActorBehaviorsInterceptor : IDiscoverableActorBehaviorsInterceptor
    {
        private readonly IReadOnlyStatDefinitionToTermMappingRepository _statDefinitionToTermMappingRepository;
        private readonly IDynamicAnimationIdentifiers _dynamicAnimationIdentifiers;

        public ActorBehaviorsInterceptor(
            IReadOnlyStatDefinitionToTermMappingRepository statDefinitionToTermMappingRepository,
            IDynamicAnimationIdentifiers dynamicAnimationIdentifiers)
        {
            _statDefinitionToTermMappingRepository = statDefinitionToTermMappingRepository;
            _dynamicAnimationIdentifiers = dynamicAnimationIdentifiers;
        }

        public void Intercept(IReadOnlyCollection<IBehavior> behaviors)
        {
            var mutableStats = behaviors.GetOnly<IHasMutableStatsBehavior>();
            mutableStats.MutateStats(stats =>
            {
                stats[_statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("LIGHT_RADIUS_RADIUS").StatDefinitionId] = 10;
                stats[_statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("LIGHT_RADIUS_INTENSITY").StatDefinitionId] = 0.5;
                stats[_statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("LIGHT_RADIUS_RED").StatDefinitionId] = 1;
                stats[_statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("LIGHT_RADIUS_GREEN").StatDefinitionId] = 0;
                stats[_statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("LIGHT_RADIUS_BLUE").StatDefinitionId] = 1;
                
                stats[_dynamicAnimationIdentifiers.AnimationSpeedMultiplierStatId] = 1;
                stats[_dynamicAnimationIdentifiers.RedMultiplierStatId] = 1;
                stats[_dynamicAnimationIdentifiers.GreenMultiplierStatId] = 1;
                stats[_dynamicAnimationIdentifiers.BlueMultiplierStatId] = 1;
                stats[_dynamicAnimationIdentifiers.AlphaMultiplierStatId] = 1;
                stats[_dynamicAnimationIdentifiers.AnimationOverrideStatId] = 0;
            });
        }
    }
}
