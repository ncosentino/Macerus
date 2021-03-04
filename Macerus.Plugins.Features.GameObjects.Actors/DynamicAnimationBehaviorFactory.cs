
using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.GameObjects.Actors.Api;

using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api;

namespace Macerus.Plugins.Features.GameObjects.Actors
{
    public sealed class DynamicAnimationBehaviorFactory : IDynamicAnimationBehaviorFactory
    {
        private readonly IStatCalculationService _statCalculationService;
        private readonly IDynamicAnimationIdentifiers _dynamicAnimationIdentifiers;

        public DynamicAnimationBehaviorFactory(
            IStatCalculationService statCalculationService,
            IDynamicAnimationIdentifiers dynamicAnimationIdentifiers)
        {
            _statCalculationService = statCalculationService;
            _dynamicAnimationIdentifiers = dynamicAnimationIdentifiers;
        }

        public IDynamicAnimationBehavior Create(string sourcePattern)
        {
            var behavior = new DynamicAnimationBehavior(
                _statCalculationService,
                _dynamicAnimationIdentifiers,
                sourcePattern);
            return behavior;
        }
    }
}
