using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;

namespace Macerus.Api.Behaviors
{
    public interface IReadOnlySpawnTemplatePropertiesBehavior : IBehavior
    {
        IReadOnlyDictionary<string, object> Properties { get; }

        IIdentifier TypeId { get; }

        IIdentifier TemplateId { get; }
    }
}
