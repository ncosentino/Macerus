using System.Collections.Generic;

using ProjectXyz.Api.GameObjects.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Static.Api
{
    public interface IReadOnlyStaticGameObjectPropertiesBehavior : IBehavior
    {
        IReadOnlyDictionary<string, object> Properties { get; }
    }
}