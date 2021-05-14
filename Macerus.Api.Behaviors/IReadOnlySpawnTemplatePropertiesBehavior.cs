using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects;

namespace Macerus.Api.Behaviors
{
    public interface IReadOnlySpawnTemplatePropertiesBehavior : IBehavior
    {
        IGameObject TemplateToSpawn { get; }
    }
}
