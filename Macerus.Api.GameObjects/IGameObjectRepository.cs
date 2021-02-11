using System.Collections.Generic;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

namespace Macerus.Api.GameObjects
{
    public interface IGameObjectRepository
    {
        IGameObject Load(
            IIdentifier typeId,
            IIdentifier objectId);

        IGameObject CreateFromTemplate(
            IIdentifier typeId,
            IIdentifier templateId,
            IReadOnlyDictionary<string, object> properties);
    }
}