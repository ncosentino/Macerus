using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.GameObjects.Containers
{
    public sealed class Container : IGameObject
    {
        public Container(IEnumerable<IBehavior> behaviors)
        {
            Behaviors = behaviors.ToArray();
        }

        public IReadOnlyCollection<IBehavior> Behaviors { get; }
    }
}
