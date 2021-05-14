using System.Collections.Generic;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace Macerus.Api.GameObjects
{
    public interface IGameObjectRepositoryAmenity
    {
        IGameObject LoadGameObject(
            IIdentifier id);

        void SaveGameObject(IGameObject gameObject);

        IGameObject CreateGameObjectFromTemplate(
            IIdentifier templateId,
            IEnumerable<IBehavior> additionalBehaviors);
    }
}