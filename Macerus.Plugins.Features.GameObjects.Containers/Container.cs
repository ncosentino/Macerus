
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.GameObjects.Containers
{
    public sealed class Container : IGameObject
    {
        public Container(IBehaviorCollection behaviors)
        {
            Behaviors = behaviors;
        }

        public IBehaviorCollection Behaviors { get; }
    }
}
