using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.GameObjects.Actors
{
    public interface IDiscoverableActorTemplateRepository : IHasFilterAttributes
    {
        IGameObject CreateFromTemplate(
            IFilterContext filterContext,
            IReadOnlyDictionary<string, object> properties);
    }
}
