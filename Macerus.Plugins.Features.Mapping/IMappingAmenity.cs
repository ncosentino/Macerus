using System.Collections.Generic;
using System.Numerics;

using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.Mapping
{
    public interface IMappingAmenity
    {
        IEnumerable<Vector2> GetAllowedPathDestinationsForActor(IGameObject actor);
    }
}