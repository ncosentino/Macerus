using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Normal
{
    public sealed class NormalItem : IGameObject
    {
        public NormalItem(IEnumerable<IBehavior> behaviors)
        {
            Behaviors = behaviors.ToArray();
        }

        public IReadOnlyCollection<IBehavior> Behaviors { get; }
    }
}
