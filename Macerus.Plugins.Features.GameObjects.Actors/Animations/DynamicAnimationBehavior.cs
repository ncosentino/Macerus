using System;
using System.Threading.Tasks;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.Animations.Api;
using Macerus.Plugins.Features.GameObjects.Actors.Api;
using Macerus.Plugins.Features.Stats.Api;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Actors
{

    public sealed class DynamicAnimationBehavior :
        BaseBehavior,
        IDynamicAnimationBehavior
    {
        private readonly ISpriteAnimationRepository _spriteAnimationRepository;
        private readonly IAnimationReplacementPatternRepository _animationReplacementPatternRepository;
        private readonly IStatCalculationServiceAmenity _statCalculationServiceAmenity;
        private readonly IDynamicAnimationIdentifiers _dynamicAnimationIdentifiers;

        private ISpriteAnimation _currentAnimation;
        private double _secondsElapsedOnFrame;
        private IIdentifier _lastAnimationId;
        private DateTime _lastLookupdUtc;
        private IIdentifier _lastSourceAnimationId;
        private IIdentifier _cachedTransformedAnimationId;
        private string _replacementPattern;
        private double _cachedAnimationSpeedMultiplier; // we update this when changing frames

        public DynamicAnimationBehavior(
            ISpriteAnimationRepository spriteAnimationRepository,
            IAnimationReplacementPatternRepository animationReplacementPatternRepository,
            IStatCalculationServiceAmenity statCalculationServiceAmenity,
            IDynamicAnimationIdentifiers dynamicAnimationIdentifiers,
            string sourcePattern,
            IIdentifier baseAnimationId,
            bool visible,
            int currentFrameIndex)
        {
            _spriteAnimationRepository = spriteAnimationRepository;
            _animationReplacementPatternRepository = animationReplacementPatternRepository;
            _statCalculationServiceAmenity = statCalculationServiceAmenity;
            _dynamicAnimationIdentifiers = dynamicAnimationIdentifiers;
            
            SourcePattern = sourcePattern;
            BaseAnimationId = baseAnimationId;
            Visible = visible;
            CurrentFrameIndex = currentFrameIndex;
            
            _cachedAnimationSpeedMultiplier = 1;
        }

        public event EventHandler<AnimationFrameEventArgs> AnimationFrameChanged;

        public string SourcePattern { get; }

        public bool Visible { get; set; }

        public int CurrentFrameIndex { get; private set; }

        public ISpriteAnimationFrame CurrentFrame => !Visible || _currentAnimation == null
            ? null
            : _currentAnimation.Frames[CurrentFrameIndex];

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
                    var overrideStatValue = _statCalculationServiceAmenity.GetStatValue(
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
                        SourcePattern,
                        _replacementPattern);
                _cachedTransformedAnimationId = transformedAnimationId;
                _lastSourceAnimationId = BaseAnimationId;
                return transformedAnimationId;
            }

            set => BaseAnimationId = value;
        }

        public async Task UpdateAnimationAsync(double secondsSinceLastFrame)
        {
            var currentAnimationId = CurrentAnimationId;
            if (_lastAnimationId != null && currentAnimationId == null)
            {
                CurrentFrameIndex = 0;
                _lastAnimationId = null;
                _secondsElapsedOnFrame = 0;
                _currentAnimation = null;

                AnimationFrameChanged?.Invoke(
                    this, 
                    new AnimationFrameEventArgs(null, null));
                return;
            }

            if (!Visible)
            {
                AnimationFrameChanged?.Invoke(
                    this,
                    new AnimationFrameEventArgs(null, null));
                return;
            }

            bool forceRefreshSprite = false;
            if (currentAnimationId != _lastAnimationId)
            {
                CurrentFrameIndex = 0;
                _secondsElapsedOnFrame = 0;
                _lastAnimationId = currentAnimationId;

                if (!_spriteAnimationRepository.TryGetAnimationById(
                    currentAnimationId,
                    out _currentAnimation))
                {
                    throw new InvalidOperationException(
                        $"The current animation ID '{currentAnimationId}' was not " +
                        $"found for '{Owner}.{this}'.");
                }

                forceRefreshSprite = true;
            }

            if (CurrentFrameIndex >= _currentAnimation.Frames.Count ||
                CurrentFrameIndex < 0)
            {
                throw new InvalidOperationException(
                    $"The current frame {CurrentFrameIndex} was out " +
                    $"of range on '{Owner}.{this}' animation " +
                    $"'{currentAnimationId}'.");
            }

            _secondsElapsedOnFrame += secondsSinceLastFrame;
            ISpriteAnimationFrame currentFrame;
            var lastFrameIndex = CurrentFrameIndex;

            while (true)
            {
                currentFrame = _currentAnimation.Frames[CurrentFrameIndex];
                if (currentFrame.DurationInSeconds == null)
                {
                    break;
                }

                var durationInSeconds = (float)(currentFrame.DurationInSeconds.Value /_cachedAnimationSpeedMultiplier);
                if (_secondsElapsedOnFrame >= durationInSeconds)
                {
                    CurrentFrameIndex++;
                    _secondsElapsedOnFrame -= durationInSeconds;

                    if (CurrentFrameIndex == _currentAnimation.Frames.Count)
                    {
                        if (_currentAnimation.Repeat)
                        {
                            CurrentFrameIndex = 0;
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

            if (CurrentFrameIndex == lastFrameIndex &&
                !forceRefreshSprite)
            {
                return;
            }

            var animationMultipliers = await GetAnimationMultipliersAsync();

            // cache this because we use it a lot internally
            _cachedAnimationSpeedMultiplier = animationMultipliers.AnimationSpeedMultiplier;
            
            AnimationFrameChanged?.Invoke(
                this,
                new AnimationFrameEventArgs(
                    currentFrame,
                    animationMultipliers));
        }

        public async Task<IAnimationMultipliers> GetAnimationMultipliersAsync()
        {
            var multiplierStats = await _statCalculationServiceAmenity.GetStatValuesAsync(
                Owner,
                new[]
                {
                    _dynamicAnimationIdentifiers.AnimationSpeedMultiplierStatId,
                    _dynamicAnimationIdentifiers.RedMultiplierStatId,
                    _dynamicAnimationIdentifiers.GreenMultiplierStatId,
                    _dynamicAnimationIdentifiers.BlueMultiplierStatId,
                    _dynamicAnimationIdentifiers.AlphaMultiplierStatId,
                });

            var multipliers = new AnimationMultipliers(
                multiplierStats[_dynamicAnimationIdentifiers.AnimationSpeedMultiplierStatId],
                multiplierStats[_dynamicAnimationIdentifiers.RedMultiplierStatId],
                multiplierStats[_dynamicAnimationIdentifiers.GreenMultiplierStatId],
                multiplierStats[_dynamicAnimationIdentifiers.BlueMultiplierStatId],
                multiplierStats[_dynamicAnimationIdentifiers.AlphaMultiplierStatId]);
            return multipliers;
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
