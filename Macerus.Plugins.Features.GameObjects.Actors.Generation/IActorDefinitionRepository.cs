using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Filtering.Api;

namespace Macerus.Plugins.Features.GameObjects.Actors.Generation
{
    public interface IActorDefinitionRepository
    {
        IEnumerable<IActorDefinition> GetActorDefinitions(IFilterContext filterContext);
    }
}
