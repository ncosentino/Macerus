using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.GameObjects.Static
{
    public sealed class StaticGameObject : IGameObject
    {
        public StaticGameObject(IBehaviorCollection behaviors)
        {
            Behaviors = behaviors;
        }

        public IBehaviorCollection Behaviors { get; }
    }
}
