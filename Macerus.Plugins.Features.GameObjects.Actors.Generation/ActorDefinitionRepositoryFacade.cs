using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors.Filtering;

namespace Macerus.Plugins.Features.GameObjects.Actors.Generation
{
    public sealed class ActorDefinitionRepositoryFacade : IActorDefinitionRepositoryFacade
    {
        private readonly IReadOnlyCollection<IDiscoverableActorDefinitionRepository> _repositories;

        public ActorDefinitionRepositoryFacade(IEnumerable<IDiscoverableActorDefinitionRepository> repositories)
        {
            _repositories = repositories.ToArray();
        }

        public IEnumerable<IActorDefinition> GetActorDefinitions(IFilterContext filterContext)
        {
            if (filterContext.MaximumCount < 1)
            {
                yield break;
            }

            var count = 0;
            foreach (var result in _repositories
                .SelectMany(x => x.GetActorDefinitions(filterContext)))
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
