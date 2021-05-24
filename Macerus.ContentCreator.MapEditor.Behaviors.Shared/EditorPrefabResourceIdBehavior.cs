
using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.ContentCreator.MapEditor.Behaviors.Shared
{
    public sealed class EditorPrefabResourceIdBehavior : BaseBehavior
    {
        public EditorPrefabResourceIdBehavior(IIdentifier prefabResourceId)
        {
            PrefabResourceId = prefabResourceId;
        }

        public IIdentifier PrefabResourceId { get; }
    }
}
