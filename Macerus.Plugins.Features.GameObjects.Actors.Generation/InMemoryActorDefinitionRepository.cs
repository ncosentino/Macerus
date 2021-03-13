using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;

namespace Macerus.Plugins.Features.GameObjects.Actors.Generation
{
    public sealed class InMemoryActorDefinitionRepository : IDiscoverableActorDefinitionRepository
    {
        private readonly IReadOnlyCollection<IActorDefinition> _actorDefinitions;
        private readonly IAttributeFilterer _attributeFilterer;

        public InMemoryActorDefinitionRepository(
            IAttributeFilterer attributeFilterer,
            IEnumerable<IActorDefinition> actorDefinitions)
        {
            _actorDefinitions = actorDefinitions.ToArray();
            _attributeFilterer = attributeFilterer;
        }

        public IEnumerable<IActorDefinition> GetActorDefinitions(IFilterContext filterContext)
        {
            var results = _attributeFilterer.Filter(
                _actorDefinitions,
                filterContext);
            return results;
        }
    }
}
