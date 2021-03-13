using System.Collections.Generic;
using System.Linq;

using Macerus.Api.GameObjects;

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

        public IEnumerable<IGameObject> CreateFromTemplate(
            IFilterContext filterContext,
            IReadOnlyDictionary<string, object> properties)
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
                .SelectMany(x => x.CreateFromTemplate(
                    filterContext,
                    properties)))
            {
                yield return result;

                count++;
                if (count >= filterContext.MaximumCount)
                {
                    break;
                }
            }
        }
    }
}
