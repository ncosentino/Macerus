using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Api.Behaviors.Filtering;

using NexusLabs.Framework;

using ProjectXyz.Api.Framework.Collections;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace Macerus.Plugins.Features.GameObjects.Actors.Generation
{
    public sealed class ActorGeneratorFacade : IActorGeneratorFacade
    {
        private readonly IReadOnlyCollection<IDiscoverableActorGenerator> _generators;
        private readonly IRandom _random;
        private readonly IFilterContextAmenity _filterContextAmenity;
        private readonly IAttributeFilterer _attributeFilterer;

        public ActorGeneratorFacade(
            IRandom random,
            IFilterContextAmenity filterContextAmenity,
            IAttributeFilterer attributeFilterer,
            IEnumerable<IDiscoverableActorGenerator> generators)
        {
            _generators = generators.ToArray();
            _random = random;
            _filterContextAmenity = filterContextAmenity;
            _attributeFilterer = attributeFilterer;
        }

        public IEnumerable<IGameObject> GenerateActors(
            IFilterContext filterContext,
            IEnumerable<IGeneratorComponent> additionalActorGeneratorComponents)
        {
            var totalCount = GetGenerationCount(
                filterContext.MinimumCount,
                filterContext.MaximumCount);
            if (totalCount < 1)
            {
                yield break;
            }

            var contextForSet = _filterContextAmenity.CreateRequiredContextForSet(
                filterContext,
                _generators);
            var filteredGenerators = _attributeFilterer.Filter(
                _generators,
                contextForSet).ToArray();
            if (!filteredGenerators.Any())
            {
                throw new InvalidOperationException(
                    $"There are no generators that match the context '{filterContext}'.");
            }

            var generatedCount = 0;
            var elligibleGenerators = new HashSet<IActorGenerator>(filteredGenerators);
            while (generatedCount < totalCount)
            {
                if (elligibleGenerators.Count < 1)
                {
                    throw new InvalidOperationException(
                        "Could not find elligible item generators with the " +
                        "provided context. Investigate the conditions on the " +
                        "context along with the item generators.");
                }

                var generator = elligibleGenerators.RandomOrDefault(_random);
                var currentContext = _filterContextAmenity.CreateFilterContextForSingle(filterContext.Attributes);

                var generatedActors = generator.GenerateActors(
                    currentContext,
                    additionalActorGeneratorComponents);
                var generatedAtLeastOne = false;
                foreach (var generatedItem in generatedActors)
                {
                    generatedCount++;
                    generatedAtLeastOne = true;
                    yield return generatedItem;
                }

                if (!generatedAtLeastOne)
                {
                    elligibleGenerators.Remove(generator);
                }
            }
        }

        private int GetGenerationCount(
            int itemCountMinimum,
            int itemCountMaximum)
        {
            var count = _random.Next(
                itemCountMinimum,
                itemCountMaximum == int.MaxValue
                    ? int.MaxValue
                    : itemCountMaximum + 1);
            return count;
        }
    }
}
