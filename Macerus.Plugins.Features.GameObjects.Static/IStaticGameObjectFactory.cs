using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.GameObjects.Static.Api;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Macerus.Plugins.Features.GameObjects.Static
{
    public interface IStaticGameObjectFactory
    {
        IGameObject Create(
            IReadOnlyTypeIdentifierBehavior typeIdentifierBehavior,
            IReadOnlyTemplateIdentifierBehavior templateIdentifierBehavior,
            IReadOnlyIdentifierBehavior identifierBehavior,
            IReadOnlyWorldLocationBehavior worldLocationBehavior,
            IReadOnlyStaticGameObjectPropertiesBehavior propertiesBehavior,
            IReadOnlyPrefabResourceIdBehavior prefabResourceIdBehavior);
    }
}