using System;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.Animations.Api;
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
        private readonly ISpriteAnimationProvider _spriteAnimationProvider;
        private readonly IAnimationReplacementPatternRepository _animationReplacementPatternRepository;
        private readonly IStatCalculationService _statCalculationService;
        private readonly IDynamicAnimationIdentifiers _dynamicAnimationIdentifiers;
        private readonly string _sourcePattern;
        
        private int _currentFrameIndex;
        private double _secondsElapsedOnFrame;
        private IIdentifier _lastAnimationId;
        private DateTime _lastLookupdUtc;
        private IIdentifier _lastSourceAnimationId;
        private IIdentifier _cachedTransformedAnimationId;
        private string _replacementPattern;

        public DynamicAnimationBehavior(
            ISpriteAnimationProvider spriteAnimationProvider,
            IAnimationReplacementPatternRepository animationReplacementPatternRepository,
            IStatCalculationService statCalculationService,
            IDynamicAnimationIdentifiers dynamicAnimationIdentifiers,
            string sourcePattern)
        {
            _spriteAnimationProvider = spriteAnimationProvider;
            _animationReplacementPatternRepository = animationReplacementPatternRepository;
            _statCalculationService = statCalculationService;
            _dynamicAnimationIdentifiers = dynamicAnimationIdentifiers;
            _sourcePattern = sourcePattern;
        }

        public event EventHandler<AnimationFrameEventArgs> AnimationFrameChanged;

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

        public void UpdateAnimation(double secondsSinceLastFrame)
        {
            var currentAnimationId = CurrentAnimationId;
            if (_lastAnimationId!= null && currentAnimationId == null)
            {
                _currentFrameIndex = 0;
                _lastAnimationId = null;
                _secondsElapsedOnFrame = 0;
                
                AnimationFrameChanged?.Invoke(
                    this, 
                    new AnimationFrameEventArgs(null));
                return;
            }

            bool forceRefreshSprite = false;
            if (currentAnimationId != _lastAnimationId)
            {
                _currentFrameIndex = 0;
                _secondsElapsedOnFrame = 0;
                _lastAnimationId = currentAnimationId;
                forceRefreshSprite = true;
            }

            if (!_spriteAnimationProvider.TryGetAnimationById(
                currentAnimationId,
                out var currentAnimation))
            {
                throw new InvalidOperationException(
                    $"The current animation ID '{currentAnimationId}' was not " +
                    $"found for '{Owner}.{this}'.");
            }

            if (_currentFrameIndex >= currentAnimation.Frames.Count ||
                _currentFrameIndex < 0)
            {
                throw new InvalidOperationException(
                    $"The current frame {_currentFrameIndex} was out " +
                    $"of range on '{Owner}.{this}' animation " +
                    $"'{currentAnimationId}'.");
            }

            _secondsElapsedOnFrame += secondsSinceLastFrame;
            ISpriteAnimationFrame currentFrame;
            var lastFrameIndex = _currentFrameIndex;

            var animationSpeedMultiplier = AnimationSpeedMultiplier ?? 1;

            while (true)
            {
                currentFrame = currentAnimation.Frames[_currentFrameIndex];
                if (currentFrame.DurationInSeconds == null)
                {
                    break;
                }

                var durationInSeconds = (float)(currentFrame.DurationInSeconds.Value / animationSpeedMultiplier);
                if (_secondsElapsedOnFrame >= durationInSeconds)
                {
                    _currentFrameIndex++;
                    _secondsElapsedOnFrame -= durationInSeconds;

                    if (_currentFrameIndex == currentAnimation.Frames.Count)
                    {
                        if (currentAnimation.Repeat)
                        {
                            _currentFrameIndex = 0;
                        }
                        else
                        {
                            CurrentAnimationId = null;
                            return;
                        }
                    }

                    continue;
                }

                break;
            }

            if (_currentFrameIndex == lastFrameIndex &&
                !forceRefreshSprite)
            {
                return;
            }

            AnimationFrameChanged?.Invoke(
                this,
                new AnimationFrameEventArgs(currentFrame));
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
