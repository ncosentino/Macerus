using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.Animations.Api;
using Macerus.Plugins.Features.GameObjects.Actors.Api;
using Macerus.Plugins.Features.Stats.Api;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.GameObjects.Actors
{
    public sealed class DynamicAnimationBehaviorFactory : IDynamicAnimationBehaviorFactory
    {
        private readonly ISpriteAnimationRepository _spriteAnimationProvider;
        private readonly IAnimationReplacementPatternRepository _animationReplacementPatternRepository;
        private readonly IStatCalculationServiceAmenity _statCalculationServiceAmenity;
        private readonly IDynamicAnimationIdentifiers _dynamicAnimationIdentifiers;

        public DynamicAnimationBehaviorFactory(
            ISpriteAnimationRepository spriteAnimationProvider,
            IAnimationReplacementPatternRepository animationReplacementPatternRepository,
            IStatCalculationServiceAmenity statCalculationServiceAmenity,
            IDynamicAnimationIdentifiers dynamicAnimationIdentifiers)
        {
            _spriteAnimationProvider = spriteAnimationProvider;
            _animationReplacementPatternRepository = animationReplacementPatternRepository;
            _statCalculationServiceAmenity = statCalculationServiceAmenity;
            _dynamicAnimationIdentifiers = dynamicAnimationIdentifiers;
        }

        public IDynamicAnimationBehavior Create(
            string sourcePattern,
            IIdentifier baseAnimationId,
            bool visible,
            int currentFrameIndex)
        {
            var behavior = new DynamicAnimationBehavior(
                _spriteAnimationProvider,
                _animationReplacementPatternRepository,
                _statCalculationServiceAmenity,
                _dynamicAnimationIdentifiers,
                sourcePattern,
                baseAnimationId,
                visible,
                currentFrameIndex);
            return behavior;
        }
    }
}
