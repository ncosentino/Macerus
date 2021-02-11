using System.Collections.Generic;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

namespace Macerus.Api.GameObjects
{
    public delegate IGameObject CreateObjectFromTemplateDelegate(
        IIdentifier typeId,
        IIdentifier objectId,
        IReadOnlyDictionary<string, object> properties);
}