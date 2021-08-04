using System.Collections.Generic;
using System.Threading.Tasks;

using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.Encounters
{
    public interface IEncounterGameObjectPlacer
    {
        Task PlaceGameObjectsAsync(IEnumerable<IGameObject> gameObjectsToPlace);
    }
}
