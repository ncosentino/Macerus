using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering;

namespace Macerus.Plugins.Features.GameObjects.Actors.Generation
{
    public interface IActorDefinitionRepository
    {
        IEnumerable<IActorDefinition> GetActorDefinitions(IFilterContext filterContext);
    }
}
