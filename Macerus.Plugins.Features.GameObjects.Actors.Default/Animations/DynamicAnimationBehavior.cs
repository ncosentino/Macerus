using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.Animations.Api;
using Macerus.Plugins.Features.Stats.Api;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Actors.Default.Animations
{
    public sealed class DynamicAnimationBehavior :
        BaseBehavior,
        IDynamicAnimationBehavior
    {
        private readonly ISpriteAnimationRepository _spriteAnimationRepository;
        private readonly IAnimationIdReplacementFacade _animationIdReplacementFacade;
        private readonly IStatCalculationServiceAmenity _statCalculationServiceAmenity;
        private readonly IDynamicAnimationIdentifiers _dynamicAnimationIdentifiers;

        private ISpriteAnimation _currentAnimation;
        private double _secondsElapsedOnFrame;
        private IIdentifier _lastAnimationId;
        private double _cachedAnimationSpeedMultiplier; // we update this when changing frames

        public DynamicAnimationBehavior(
            ISpriteAnimationRepository spriteAnimationRepository,
            IAnimationIdReplacementFacade animationIdReplacementFacade,
            IStatCalculationServiceAmenity statCalculationServiceAmenity,
            IDynamicAnimationIdentifiers dynamicAnimationIdentifiers,
            IIdentifier baseAnimationId,
            bool visible,
            int currentFrameIndex)
        {
            _spriteAnimationRepository = spriteAnimationRepository;
            _animationIdReplacementFacade = animationIdReplacementFacade;
            _statCalculationServiceAmenity = statCalculationServiceAmenity;
            _dynamicAnimationIdentifiers = dynamicAnimationIdentifiers;

            BaseAnimationId = baseAnimationId;
            Visible = visible;
            CurrentFrameIndex = currentFrameIndex;

            _cachedAnimationSpeedMultiplier = 1;
        }

        public event EventHandler<AnimationFrameEventArgs> AnimationFrameChanged;

        public bool Visible { get; set; }

        public int CurrentFrameIndex { get; private set; }

        public ISpriteAnimationFrame CurrentFrame => !Visible || _currentAnimation == null
            ? null
            : _currentAnimation.Frames[CurrentFrameIndex];

        public IIdentifier BaseAnimationId { get; set; }

        public async Task UpdateAnimationAsync(double secondsSinceLastFrame)
        {
            var currentAnimationId = await GetCurrentAnimationIdAsync().ConfigureAwait(false);
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

            if (currentAnimationId == null ||
                !Visible)
            {
                AnimationFrameChanged?.Invoke(
                    this,
                    new AnimationFrameEventArgs(null, null));
                return;
            }

            var forceRefreshSprite = false;
            if (!Equals(currentAnimationId, _lastAnimationId))
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

            if (_currentAnimation == null)
            {
                AnimationFrameChanged?.Invoke(
                    this,
                    new AnimationFrameEventArgs(null, null));
                return;
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

                var durationInSeconds = (float)(currentFrame.DurationInSeconds.Value / _cachedAnimationSpeedMultiplier);
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
                            BaseAnimationId = null;
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

            var animationMultipliers = await GetAnimationMultipliersAsync().ConfigureAwait(false);

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
            var multiplierStats = await _statCalculationServiceAmenity
                .GetStatValuesAsync(
                    Owner,
                    new[]
                    {
                        _dynamicAnimationIdentifiers.AnimationSpeedMultiplierStatId,
                        _dynamicAnimationIdentifiers.RedMultiplierStatId,
                        _dynamicAnimationIdentifiers.GreenMultiplierStatId,
                        _dynamicAnimationIdentifiers.BlueMultiplierStatId,
                        _dynamicAnimationIdentifiers.AlphaMultiplierStatId,
                    })
                .ConfigureAwait(false);

            var multipliers = new AnimationMultipliers(
                multiplierStats[_dynamicAnimationIdentifiers.AnimationSpeedMultiplierStatId],
                multiplierStats[_dynamicAnimationIdentifiers.RedMultiplierStatId],
                multiplierStats[_dynamicAnimationIdentifiers.GreenMultiplierStatId],
                multiplierStats[_dynamicAnimationIdentifiers.BlueMultiplierStatId],
                multiplierStats[_dynamicAnimationIdentifiers.AlphaMultiplierStatId]);
            return multipliers;
        }

        private async Task<IIdentifier> GetCurrentAnimationIdAsync()
        {
            if (BaseAnimationId == null)
            {
                return BaseAnimationId;
            }

            var replacements = await _animationIdReplacementFacade
                .GetReplacementsAsync(this)
                .ConfigureAwait(false);
            var transformedAnimationId = Transform(
                BaseAnimationId,
                replacements);
            return transformedAnimationId;
        }

        private IIdentifier Transform(
            IIdentifier inputIdentifier,
            IEnumerable<KeyValuePair<string, string>> replacements)
        {
            var interim = inputIdentifier.ToString();
            foreach (var replacement in replacements)
            {
                interim = interim.Replace(replacement.Key, replacement.Value);
            }

            var transformedIdentifier = new StringIdentifier(interim);
            return transformedIdentifier;
        }
    }
}
