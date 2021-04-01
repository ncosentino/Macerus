using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.GameObjects.Actors.Api;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Stats;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.GameObjects.Actors
{
    public sealed class RequiredStatsActorBehaviorsInterceptor : IDiscoverableActorBehaviorsInterceptor
    {
        private readonly IReadOnlyStatDefinitionToTermMappingRepositoryFacade _statDefinitionToTermMappingRepository;
        private readonly IDynamicAnimationIdentifiers _dynamicAnimationIdentifiers;

        public RequiredStatsActorBehaviorsInterceptor(
            IReadOnlyStatDefinitionToTermMappingRepositoryFacade statDefinitionToTermMappingRepository,
            IDynamicAnimationIdentifiers dynamicAnimationIdentifiers)
        {
            _statDefinitionToTermMappingRepository = statDefinitionToTermMappingRepository;
            _dynamicAnimationIdentifiers = dynamicAnimationIdentifiers;
        }

        public int Priority => -10000;

        IEnumerable<IBehavior> IActorBehaviorsInterceptor.Intercept(IReadOnlyCollection<IBehavior> behaviors)
        {
            var mutableStats = behaviors
                .Get<IHasMutableStatsBehavior>()
                .First();
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
            return behaviors;
        }
    }
}
