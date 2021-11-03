using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Api.Behaviors.Filtering;
using Macerus.Plugins.Features.GameObjects.Items.Affixes.Api;

using NexusLabs.Framework;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Collections;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Default.Attributes;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.GameObjects.Items.Affixes.Default
{
    public sealed class RandomAffixGeneratorComponentToBehaviorConverter : IDiscoverableGeneratorComponentToBehaviorConverter
    {
        private readonly IRandom _random;
        private readonly Lazy<IReadOnlyAffixDefinitionRepositoryFacade> _lazyAffixDefinitionRepository;
        private readonly Lazy<IFilterContextAmenity> _lazyFilterContextAmenity;
        private readonly Lazy<IGeneratorComponentToBehaviorConverter> _lazyGeneratorComponentToBehaviorConverter;

        public RandomAffixGeneratorComponentToBehaviorConverter(
            IRandom random,
            Lazy<IReadOnlyAffixDefinitionRepositoryFacade> lazyAffixDefinitionRepository,
            Lazy<IFilterContextAmenity> lazyFilterContextAmenity,
            Lazy<IGeneratorComponentToBehaviorConverter> lazyGeneratorComponentToBehaviorConverter)
        {
            _random = random;
            _lazyAffixDefinitionRepository = lazyAffixDefinitionRepository;
            _lazyFilterContextAmenity = lazyFilterContextAmenity;
            _lazyGeneratorComponentToBehaviorConverter = lazyGeneratorComponentToBehaviorConverter;
        }

        public Type ComponentType => typeof(RandomAffixGeneratorComponent);

        public IEnumerable<IBehavior> Convert(
            IFilterContext filterContext,
            IEnumerable<IBehavior> baseBehaviors,
            IGeneratorComponent generatorComponent)
        {
            var randomAffixGeneratorComponent = (RandomAffixGeneratorComponent)generatorComponent;

            var attributes = filterContext
                .Attributes
                .Where(x => !randomAffixGeneratorComponent.AffixDefinitionFilter.Any(s => s.Id.Equals(x.Id)))
                .Concat(randomAffixGeneratorComponent.AffixDefinitionFilter);
            var affixGeneratorContext = _lazyFilterContextAmenity
                .Value
                .CreateFilterContext(
                    randomAffixGeneratorComponent.MinimumAffixes,
                    randomAffixGeneratorComponent.MaximumAffixes,
                    attributes);

            // FIXME: right now we have a filter context to generate affixes,
            // but affixes (currently) only generate enchantments based on
            // their ID... so we actually DONT want to pass in a filter
            // context for this since we might constrain it too much. **IF**
            // these assumptions change:
            // - we could have dynamic enchantments for affixes
            // - ...
            var noContext = _lazyFilterContextAmenity
                .Value
                .CreateNoneFilterContext();

            var targetCount = GetAffixCount(
                randomAffixGeneratorComponent.MinimumAffixes,
                randomAffixGeneratorComponent.MaximumAffixes);
            var currentCount = 0;

            var baseBehaviorSet = new HashSet<IBehavior>(baseBehaviors);
            var accumulatedBehaviors = new HashSet<IBehavior>();
            var mutexKeys = new List<IIdentifier>();
            var nextAffixGeneratorContext = !mutexKeys.Any()
                ? affixGeneratorContext
                : _lazyFilterContextAmenity
                .Value
                .CopyWithAdditionalAttributes(
                    affixGeneratorContext,
                    new[]
                    {
                        _lazyFilterContextAmenity.Value.CreateRequiredAttribute(
                            new StringIdentifier("affix-mutex-key"),
                            new NotFilterAttributeValue(new AnyIdentifierCollectionFilterAttributeValue(mutexKeys)))
                    });

            while (currentCount < targetCount)
            {
                var affixDefinitions = _lazyAffixDefinitionRepository
                    .Value
                    .GetAffixDefinitions(affixGeneratorContext);
                var affixDefinition = affixDefinitions.RandomOrDefault(_random);
                if (affixDefinition == null)
                {
                    throw new InvalidOperationException(
                        "Could not read affix definitions that meet the " +
                        "required context. Inspect the context provided and " +
                        "ensure that there are repositories configured that " +
                        "meet the criteria.");
                }

                foreach (var newBehavior in affixDefinition
                    .GeneratorComponents
                    .SelectMany(affixGeneratorComponent => _lazyGeneratorComponentToBehaviorConverter
                        .Value
                        .Convert(
                            noContext,
                            baseBehaviorSet.Concat(accumulatedBehaviors),
                            affixGeneratorComponent))
                    .Where(x => !baseBehaviorSet.Contains(x)))
                {
                    if (newBehavior is IAffixMutexBehavior affixMutexBehavior)
                    {
                        mutexKeys.Add(affixMutexBehavior.MutexKey);
                        continue; // we don't need to add this
                    }

                    accumulatedBehaviors.Add(newBehavior);
                }
                
                currentCount++;
            }

            return accumulatedBehaviors;
        }

        private int GetAffixCount(
            int affixCountMinimum,
            int affixCountMaximum)
        {
            var count = _random.Next(
                affixCountMinimum,
                affixCountMaximum == int.MaxValue
                    ? int.MaxValue
                    : affixCountMaximum + 1);
            return count;
        }
    }
}