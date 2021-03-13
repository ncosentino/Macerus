using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.GameObjects.Actors
{
    public interface IDiscoverableActorTemplateRepository : IHasFilterAttributes
    {
        IEnumerable<IGameObject> CreateFromTemplate(
            IFilterContext filterContext,
            IReadOnlyDictionary<string, object> properties);
    }
}
