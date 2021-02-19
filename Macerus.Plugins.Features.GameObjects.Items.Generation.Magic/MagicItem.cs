using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Magic
{
    public sealed class MagicItem : IGameObject
    {
        public MagicItem(IEnumerable<IBehavior> behaviors)
        {
            Behaviors = behaviors.ToArray();
        }

        public IReadOnlyCollection<IBehavior> Behaviors { get; }
    }
}