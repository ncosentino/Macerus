using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.Encounters
{
    public interface IEncounterGameObjectPlacer
    {
        void PlaceGameObjects(IEnumerable<IGameObject> gameObjectsToPlace);
    }
}
