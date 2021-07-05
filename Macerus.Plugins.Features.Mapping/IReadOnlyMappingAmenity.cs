using System.Collections.Generic;
using System.Numerics;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Mapping;

namespace Macerus.Plugins.Features.Mapping
{
    public interface IReadOnlyMappingAmenity
    {
        IPathFinder CurrentPathFinder { get; }

        IReadOnlyCollection<IGameObject> GameObjects { get; }

        IEnumerable<Vector2> GetAllowedPathDestinationsForActor(IGameObject actor);
    }
}