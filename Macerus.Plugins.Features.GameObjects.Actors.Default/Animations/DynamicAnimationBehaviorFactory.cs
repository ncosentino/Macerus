using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.Animations.Api;
using Macerus.Plugins.Features.Stats;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.GameObjects.Actors.Default.Animations
{
    public sealed class DynamicAnimationBehaviorFactory : IDynamicAnimationBehaviorFactory
    {
        private readonly ISpriteAnimationRepository _spriteAnimationProvider;
        private readonly IAnimationIdReplacementFacade _animationIdReplacementFacade;
        private readonly IStatCalculationServiceAmenity _statCalculationServiceAmenity;
        private readonly IDynamicAnimationIdentifiers _dynamicAnimationIdentifiers;

        public DynamicAnimationBehaviorFactory(
            ISpriteAnimationRepository spriteAnimationProvider,
            IAnimationIdReplacementFacade animationIdReplacementFacade,
            IStatCalculationServiceAmenity statCalculationServiceAmenity,
            IDynamicAnimationIdentifiers dynamicAnimationIdentifiers)
        {
            _spriteAnimationProvider = spriteAnimationProvider;
            _animationIdReplacementFacade = animationIdReplacementFacade;
            _statCalculationServiceAmenity = statCalculationServiceAmenity;
            _dynamicAnimationIdentifiers = dynamicAnimationIdentifiers;
        }

        public IDynamicAnimationBehavior Create(
            IIdentifier baseAnimationId,
            bool visible,
            int currentFrameIndex)
        {
            var behavior = new DynamicAnimationBehavior(
                _spriteAnimationProvider,
                _animationIdReplacementFacade,
                _statCalculationServiceAmenity,
                _dynamicAnimationIdentifiers,
                baseAnimationId,
                visible,
                currentFrameIndex);
            return behavior;
        }
    }
}
