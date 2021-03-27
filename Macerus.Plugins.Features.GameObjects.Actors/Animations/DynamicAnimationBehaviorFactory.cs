using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.Animations.Api;
using Macerus.Plugins.Features.GameObjects.Actors.Api;

using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api;

namespace Macerus.Plugins.Features.GameObjects.Actors
{
    public sealed class DynamicAnimationBehaviorFactory : IDynamicAnimationBehaviorFactory
    {
        private readonly ISpriteAnimationRepository _spriteAnimationProvider;
        private readonly IAnimationReplacementPatternRepository _animationReplacementPatternRepository;
        private readonly IStatCalculationService _statCalculationService;
        private readonly IDynamicAnimationIdentifiers _dynamicAnimationIdentifiers;

        public DynamicAnimationBehaviorFactory(
            ISpriteAnimationRepository spriteAnimationProvider,
            IAnimationReplacementPatternRepository animationReplacementPatternRepository,
            IStatCalculationService statCalculationService,
            IDynamicAnimationIdentifiers dynamicAnimationIdentifiers)
        {
            _spriteAnimationProvider = spriteAnimationProvider;
            _animationReplacementPatternRepository = animationReplacementPatternRepository;
            _statCalculationService = statCalculationService;
            _dynamicAnimationIdentifiers = dynamicAnimationIdentifiers;
        }

        public IDynamicAnimationBehavior Create(string sourcePattern)
        {
            var behavior = new DynamicAnimationBehavior(
                _spriteAnimationProvider,
                _animationReplacementPatternRepository,
                _statCalculationService,
                _dynamicAnimationIdentifiers,
                sourcePattern);
            return behavior;
        }
    }
}
