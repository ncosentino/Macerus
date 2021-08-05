using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.Stats;

using NexusLabs.Collections.Generic;

using ProjectXyz.Api.GameObjects.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Actors.Default.Animations
{
    public sealed class BaseAnimationIdByStatReplacement : IDiscoverableAnimationIdReplacement
    {
        private readonly IAnimationReplacementPatternRepository _animationReplacementPatternRepository;
        private readonly IStatCalculationServiceAmenity _statCalculationServiceAmenity;
        private readonly IDynamicAnimationIdentifiers _dynamicAnimationIdentifiers;
        private readonly IMacerusActorIdentifiers _actorIdentifiers;
        private readonly ICache<IBehavior, CacheEntry> _cachedReplacementPatternsForBehaviors;

        public BaseAnimationIdByStatReplacement(
            IAnimationReplacementPatternRepository animationReplacementPatternRepository,
            IStatCalculationServiceAmenity statCalculationServiceAmenity,
            IDynamicAnimationIdentifiers dynamicAnimationIdentifiers,
            IMacerusActorIdentifiers actorIdentifiers)
        {
            _animationReplacementPatternRepository = animationReplacementPatternRepository;
            _statCalculationServiceAmenity = statCalculationServiceAmenity;
            _dynamicAnimationIdentifiers = dynamicAnimationIdentifiers;
            _actorIdentifiers = actorIdentifiers;
            _cachedReplacementPatternsForBehaviors = new LruCache<IBehavior, CacheEntry>(100);
        }

        public async Task<IReadOnlyCollection<KeyValuePair<string, string>>> GetReplacementsAsync(IReadOnlyDynamicAnimationBehavior dynamicAnimationBehavior)
        {
            string replacementPattern;
            if (!_cachedReplacementPatternsForBehaviors.TryGet(
                dynamicAnimationBehavior,
                out var cachedEntry) ||
                DateTime.UtcNow - cachedEntry.LastLookupUtc >= TimeSpan.FromSeconds(1))
            {
                var overrideStatValue = await _statCalculationServiceAmenity
                    .GetStatValueAsync(
                        dynamicAnimationBehavior.Owner,
                        _dynamicAnimationIdentifiers.AnimationOverrideStatId)
                    .ConfigureAwait(false);
                replacementPattern = _animationReplacementPatternRepository.GetReplacementPattern((int)overrideStatValue);
                _cachedReplacementPatternsForBehaviors.Add(dynamicAnimationBehavior, new CacheEntry(DateTime.UtcNow, replacementPattern));
            }
            else
            {
                replacementPattern = cachedEntry.ReplacementPattern;
            }

            return new[]
            {
                new KeyValuePair<string, string>(
                    _actorIdentifiers.AnimationActorPlaceholder.ToString(),
                    replacementPattern),
            };
        }

        private sealed class CacheEntry
        {
            public CacheEntry(DateTime lastLookupUtc, string replacementPattern)
            {
                LastLookupUtc = lastLookupUtc;
                ReplacementPattern = replacementPattern;
            }

            public DateTime LastLookupUtc { get; }

            public string ReplacementPattern { get; }
        }
    }
}
