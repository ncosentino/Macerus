using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.GameObjects.Actors.Generation
{
    public interface IActorGenerator
    {
        IEnumerable<IGameObject> GenerateActors(IFilterContext filterContext);
    }
}