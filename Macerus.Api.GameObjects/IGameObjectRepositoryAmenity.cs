using System.Collections.Generic;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

namespace Macerus.Api.GameObjects
{
    public interface IGameObjectRepositoryAmenity
    {
        IGameObject LoadGameObject(
            IIdentifier id);

        void SaveGameObject(IGameObject gameObject);

        IGameObject CreateGameObjectFromTemplate(
            IIdentifier typeId,
            IIdentifier templateId,
            IReadOnlyDictionary<string, object> properties);
    }
}