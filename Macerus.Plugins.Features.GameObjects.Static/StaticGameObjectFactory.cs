using System.Linq;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.GameObjects.Static.Api;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Macerus.Plugins.Features.GameObjects.Static
{
    public sealed class StaticGameObjectFactory : IStaticGameObjectFactory
    {
        private readonly IBehaviorManager _behaviorManager;
        private readonly IStaticGameObjectBehaviorsProviderFacade _staticGameObjectBehaviorsProviderFacade;
        private readonly IStaticGameObjectBehaviorsInterceptorFacade _staticGameObjectBehaviorsInterceptorFacade;

        public StaticGameObjectFactory(
            IBehaviorManager behaviorManager,
            IStaticGameObjectBehaviorsProviderFacade staticGameObjectBehaviorsProviderFacade,
            IStaticGameObjectBehaviorsInterceptorFacade staticGameObjectBehaviorsInterceptorFacade)
        {
            _behaviorManager = behaviorManager;
            _staticGameObjectBehaviorsProviderFacade = staticGameObjectBehaviorsProviderFacade;
            _staticGameObjectBehaviorsInterceptorFacade = staticGameObjectBehaviorsInterceptorFacade;
        }

        public IGameObject Create(
            IReadOnlyTypeIdentifierBehavior typeIdentifierBehavior,
            IReadOnlyTemplateIdentifierBehavior templateIdentifierBehavior,
            IReadOnlyIdentifierBehavior identifierBehavior,
            IReadOnlyWorldLocationBehavior worldLocationBehavior,
            IReadOnlyStaticGameObjectPropertiesBehavior propertiesBehavior,
            IReadOnlyPrefabResourceIdBehavior prefabResourceIdBehavior)
        {
            var baseBehaviours = new IBehavior[]
            {
                typeIdentifierBehavior,
                templateIdentifierBehavior,
                identifierBehavior,
                worldLocationBehavior,
                propertiesBehavior,
                prefabResourceIdBehavior,
            };
            var additionalBehaviors = _staticGameObjectBehaviorsProviderFacade.GetBehaviors(baseBehaviours);
            var allBehaviors = baseBehaviours
                .Concat(additionalBehaviors)
                .ToArray();
            _staticGameObjectBehaviorsInterceptorFacade.Intercept(allBehaviors);

            var staticGameObject = new StaticGameObject(allBehaviors);
            _behaviorManager.Register(staticGameObject, allBehaviors);
            return staticGameObject;
        }
    }
}
