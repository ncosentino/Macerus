using System.Linq;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.GameObjects.Static.Api;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Macerus.Plugins.Features.GameObjects.Static
{
    public sealed class StaticGameObjectFactory : IStaticGameObjectFactory
    {
        private readonly IGameObjectFactory _gameObjectFactory;
        private readonly IStaticGameObjectBehaviorsProviderFacade _staticGameObjectBehaviorsProviderFacade;
        private readonly IStaticGameObjectBehaviorsInterceptorFacade _staticGameObjectBehaviorsInterceptorFacade;

        public StaticGameObjectFactory(
            IGameObjectFactory gameObjectFactory,
            IStaticGameObjectBehaviorsProviderFacade staticGameObjectBehaviorsProviderFacade,
            IStaticGameObjectBehaviorsInterceptorFacade staticGameObjectBehaviorsInterceptorFacade)
        {
            _gameObjectFactory = gameObjectFactory;
            _staticGameObjectBehaviorsProviderFacade = staticGameObjectBehaviorsProviderFacade;
            _staticGameObjectBehaviorsInterceptorFacade = staticGameObjectBehaviorsInterceptorFacade;
        }

        public IGameObject Create(
            IReadOnlyTypeIdentifierBehavior typeIdentifierBehavior,
            IReadOnlyTemplateIdentifierBehavior templateIdentifierBehavior,
            IReadOnlyIdentifierBehavior identifierBehavior,
            IPositionBehavior positionBehavior,
            ISizeBehavior sizeBehavior,
            IReadOnlyStaticGameObjectPropertiesBehavior propertiesBehavior,
            IReadOnlyPrefabResourceIdBehavior prefabResourceIdBehavior)
        {
            var baseBehaviours = new IBehavior[]
            {
                typeIdentifierBehavior,
                templateIdentifierBehavior,
                identifierBehavior,
                positionBehavior,
                sizeBehavior,
                propertiesBehavior,
                prefabResourceIdBehavior,
            };
            var additionalBehaviors = _staticGameObjectBehaviorsProviderFacade.GetBehaviors(baseBehaviours);
            var allBehaviors = baseBehaviours
                .Concat(additionalBehaviors)
                .ToArray();
            _staticGameObjectBehaviorsInterceptorFacade.Intercept(allBehaviors);

            var staticGameObject = _gameObjectFactory.Create(allBehaviors);
            return staticGameObject;
        }
    }
}
