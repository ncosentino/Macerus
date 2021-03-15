using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Api.GameObjects;

using NexusLabs.Contracts;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.GameObjects;

namespace Macerus.Game.GameObjects
{
    public sealed class GameObjectRepositoryFacade : IGameObjectRepositoryFacade
    {
        private readonly IReadOnlyCollection<IDiscoverableGameObjectRepository> _repositories;
        private readonly IAttributeFilterer _attributeFilterer;

        public GameObjectRepositoryFacade(
            IAttributeFilterer attributeFilterer,
            IEnumerable<IDiscoverableGameObjectRepository> discoverableGameObjectRepositories)
        {
            _repositories = discoverableGameObjectRepositories.ToArray();
            _attributeFilterer = attributeFilterer;
        }

        public IEnumerable<IGameObject> Load(IFilterContext filterContext)
        {
            if (filterContext.MaximumCount < 1)
            {
                yield break;
            }

            var filteredRepositories = _attributeFilterer.Filter(
                _repositories,
                filterContext);

            var count = 0;
            foreach (var result in filteredRepositories
                .SelectMany(x => x.Load(filterContext)))
            {
                yield return result;

                count++;
                if (count >= filterContext.MaximumCount)
                {
                    break;
                }
            }
        }

        public IGameObject CreateFromTemplate(
            IFilterContext filterContext,
            IReadOnlyDictionary<string, object> properties)
        {
            Contract.Requires(
                filterContext.MinimumCount == 1,
                $"Expecting minimum count to be 1 but was {filterContext.MinimumCount}.");
            Contract.Requires(
                filterContext.MaximumCount == 1,
                $"Expecting maximum count to be 1 but was {filterContext.MaximumCount}.");

            var filteredRepositories = _attributeFilterer
                .Filter(
                    _repositories,
                    filterContext)
                .ToArray();
            if (filteredRepositories.Length < 1)
            {
                throw new InvalidOperationException(
                    $"Unable to create object from template. There were no " +
                    $"repositories that matched filter context '{filterContext}'.");
            }

            var results = filteredRepositories
                .Select(x => x.CreateFromTemplate(
                    filterContext,
                    properties))
                .ToArray();

            if (results.Length != 1)
            {
                throw new InvalidOperationException(
                    $"Expecting only one game object to be created from " +
                    $"template but {results.Length} were created.");
            }

            return results.Single();
        }
    }
}
