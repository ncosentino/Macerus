using Macerus.Api.Behaviors;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Shared.Behaviors
{
    public sealed class SpawnTemplatePropertiesBehavior :
        BaseBehavior,
        IReadOnlySpawnTemplatePropertiesBehavior
    {
        public SpawnTemplatePropertiesBehavior(IGameObject templateToSpawn)
        {
            TemplateToSpawn = templateToSpawn;
        }

        public IGameObject TemplateToSpawn { get; }
    }
}
