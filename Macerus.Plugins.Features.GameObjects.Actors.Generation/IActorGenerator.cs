using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;

namespace Macerus.Plugins.Features.GameObjects.Actors.Generation
{
    public interface IActorGenerator
    {
        IEnumerable<IGameObject> GenerateActors(
            IFilterContext filterContext,
            IEnumerable<IGeneratorComponent> additionalActorGeneratorComponents);
    }
}