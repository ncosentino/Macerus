using System;
using System.Threading.Tasks;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.Animations.Api;
using Macerus.Plugins.Features.GameObjects.Actors.Api;
using Macerus.Plugins.Features.Stats;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
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
        private readonly string _sourcePattern;

        private int _currentFrameIndex;
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
            string sourcePattern)
        {
            _spriteAnimationRepository = spriteAnimationRepository;
            _animationReplacementPatternRepository = animationReplacementPatternRepository;
            _statCalculationServiceAmenity = statCalculationServiceAmenity;
            _dynamicAnimationIdentifiers = dynamicAnimationIdentifiers;
            _sourcePattern = sourcePattern;
            _cachedAnimationSpeedMultiplier = 1;
        }

        public event EventHandler<AnimationFrameEventArgs> AnimationFrameChanged;

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
                        (IGameObject)Owner,
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

        // FIXME: async void... start propagating this up!
        public async void UpdateAnimation(double secondsSinceLastFrame)
        {
            var currentAnimationId = CurrentAnimationId;
            if (_lastAnimationId!= null && currentAnimationId == null)
            {
                _currentFrameIndex = 0;
                _lastAnimationId = null;
                _secondsElapsedOnFrame = 0;
                
                AnimationFrameChanged?.Invoke(
                    this, 
                    new AnimationFrameEventArgs(null, null));
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

            if (!_spriteAnimationRepository.TryGetAnimationById(
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

            while (true)
            {
                currentFrame = currentAnimation.Frames[_currentFrameIndex];
                if (currentFrame.DurationInSeconds == null)
                {
                    break;
                }

                var durationInSeconds = (float)(currentFrame.DurationInSeconds.Value /_cachedAnimationSpeedMultiplier);
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

            var animationMultipliers = await GetAnimationMultipliersAsync();

            // cache this because we use it a lot internally
            _cachedAnimationSpeedMultiplier = animationMultipliers.AnimationSpeedMultiplier;
            
            AnimationFrameChanged?.Invoke(
                this,
                new AnimationFrameEventArgs(
                    currentFrame,
                    animationMultipliers));
        }

        private async Task<IAnimationMultipliers> GetAnimationMultipliersAsync()
        {
            var multiplierStats = await _statCalculationServiceAmenity.GetStatValuesAsync(
                (IGameObject)Owner,
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
