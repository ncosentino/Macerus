using System;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.GameObjects.Actors.Api;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Actors
{
    public sealed class DynamicAnimationBehavior :
        BaseBehavior,
        IDynamicAnimationBehavior
    {
        private readonly IAnimationReplacementPatternRepository _animationReplacementPatternRepository;
        private readonly IStatCalculationService _statCalculationService;
        private readonly IDynamicAnimationIdentifiers _dynamicAnimationIdentifiers;
        private readonly string _sourcePattern;

        public DynamicAnimationBehavior(
            IAnimationReplacementPatternRepository animationReplacementPatternRepository,
            IStatCalculationService statCalculationService,
            IDynamicAnimationIdentifiers dynamicAnimationIdentifiers,
            string sourcePattern)
        {
            _animationReplacementPatternRepository = animationReplacementPatternRepository;
            _statCalculationService = statCalculationService;
            _dynamicAnimationIdentifiers = dynamicAnimationIdentifiers;
            _sourcePattern = sourcePattern;
        }

        private DateTime _lastLookupdUtc;
        private IIdentifier _lastSourceAnimationId;
        private IIdentifier _cachedTransformedAnimationId;
        private string _replacementPattern;

        public double? AnimationSpeedMultiplier => _statCalculationService.GetStatValue(
            Owner,
            _dynamicAnimationIdentifiers.AnimationSpeedMultiplierStatId);

        public double? RedMultiplier => _statCalculationService.GetStatValue(
            Owner,
            _dynamicAnimationIdentifiers.RedMultiplierStatId);

        public double? GreenMultiplier => _statCalculationService.GetStatValue(
            Owner,
            _dynamicAnimationIdentifiers.GreenMultiplierStatId);

        public double? BlueMultiplier => _statCalculationService.GetStatValue(
            Owner,
            _dynamicAnimationIdentifiers.BlueMultiplierStatId);

        public double? AlphaMultiplier => _statCalculationService.GetStatValue(
            Owner,
            _dynamicAnimationIdentifiers.AlphaMultiplierStatId);

        public IIdentifier BaseAnimationId { get; set; }

        public IIdentifier CurrentAnimationId
        {
            get
            {
                if (BaseAnimationId == null)
                {
                    return BaseAnimationId;
                }

                if (DateTime.UtcNow - _lastLookupdUtc > TimeSpan.FromSeconds(1))
                {
                    var overrideStatValue = _statCalculationService.GetStatValue(
                        Owner,
                        _dynamicAnimationIdentifiers.AnimationOverrideStatId);
                    _replacementPattern = _animationReplacementPatternRepository
                        .GetReplacementPattern((int)overrideStatValue);
                    _lastLookupdUtc = DateTime.UtcNow;
                }

                var transformedAnimationId = BaseAnimationId.Equals(_lastSourceAnimationId)
                    ? _cachedTransformedAnimationId
                    : Transform(
                        BaseAnimationId,
                        _sourcePattern,
                        _replacementPattern);
                _cachedTransformedAnimationId = transformedAnimationId;
                _lastSourceAnimationId = BaseAnimationId;
                return transformedAnimationId;
            }

            set => BaseAnimationId = value;
        }

        private IIdentifier Transform(
            IIdentifier inputIdentifier,
            string sourcePattern,
            string replacementPattern)
        {
            var transformedIdentifier = new StringIdentifier(
                inputIdentifier
                    .ToString()
                    .Replace(sourcePattern, replacementPattern));
            return transformedIdentifier;
        }
    }
}
