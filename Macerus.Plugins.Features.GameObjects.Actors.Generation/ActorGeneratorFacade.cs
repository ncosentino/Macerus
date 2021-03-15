using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Api.Behaviors.Filtering;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.GameObjects.Actors.Generation
{
    public sealed class ActorGeneratorFacade : IActorGeneratorFacade
    {
        private readonly IReadOnlyCollection<IDiscoverableActorGenerator> _generators;
        private readonly IFilterContextAmenity _filterContextAmenity;
        private readonly IAttributeFilterer _attributeFilterer;

        public ActorGeneratorFacade(
            IFilterContextAmenity filterContextAmenity,
            IAttributeFilterer attributeFilterer,
            IEnumerable<IDiscoverableActorGenerator> generators)
        {
            _generators = generators.ToArray();
            _filterContextAmenity = filterContextAmenity;
            _attributeFilterer = attributeFilterer;
        }

        public IEnumerable<IGameObject> GenerateActors(IFilterContext filterContext)
        {
            if (filterContext.MaximumCount < 1)
            {
                yield break;
            }

            var contextForSet = _filterContextAmenity.CreateRequiredContextForSet(
                filterContext,
                _generators);
            var filteredGenerators = _attributeFilterer.Filter(
                _generators,
                contextForSet);

            var count = 0;
            foreach (var result in filteredGenerators
                .SelectMany(x => x.GenerateActors(filterContext)))
            {
                yield return result;
                count++;

                if (count >= filterContext.MaximumCount)
                {
                    yield break;
                }
            }

            if (count < filterContext.MinimumCount)
            {
                throw new InvalidOperationException(
                    $"Filter context had requested {filterContext.MinimumCount} " +
                    $"- {filterContext.MaximumCount} results but only {count} " +
                    $"were returned.");
            }
        }
    }
}
