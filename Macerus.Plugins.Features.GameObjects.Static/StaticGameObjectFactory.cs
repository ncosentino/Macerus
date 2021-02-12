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
        private readonly IBehaviorCollectionFactory _behaviorCollectionFactory;
        private readonly IBehaviorManager _behaviorManager;
        private readonly IStaticGameObjectBehaviorsProviderFacade _staticGameObjectBehaviorsProviderFacade;
        private readonly IStaticGameObjectBehaviorsInterceptorFacade _staticGameObjectBehaviorsInterceptorFacade;

        public StaticGameObjectFactory(
            IBehaviorCollectionFactory behaviorCollectionFactory,
            IBehaviorManager behaviorManager,
            IStaticGameObjectBehaviorsProviderFacade staticGameObjectBehaviorsProviderFacade,
            IStaticGameObjectBehaviorsInterceptorFacade staticGameObjectBehaviorsInterceptorFacade)
        {
            _behaviorCollectionFactory = behaviorCollectionFactory;
            _behaviorManager = behaviorManager;
            _staticGameObjectBehaviorsProviderFacade = staticGameObjectBehaviorsProviderFacade;
            _staticGameObjectBehaviorsInterceptorFacade = staticGameObjectBehaviorsInterceptorFacade;
        }

        public IGameObject Create(
            IReadOnlyTypeIdentifierBehavior typeIdentifierBehavior,
            IReadOnlyTemplateIdentifierBehavior templateIdentifierBehavior,
            IReadOnlyIdentifierBehavior identifierBehavior,
            IReadOnlyWorldLocationBehavior worldLocationBehavior,
            IReadOnlyStaticGameObjectPropertiesBehavior propertiesBehavior)
        {
            var baseBehaviours = new IBehavior[]
            {
                typeIdentifierBehavior,
                templateIdentifierBehavior,
                identifierBehavior,
                worldLocationBehavior,
                propertiesBehavior,
            };
            var additionalBehaviors = _staticGameObjectBehaviorsProviderFacade.GetBehaviors(baseBehaviours);
            var allBehaviors = _behaviorCollectionFactory
                .Create(baseBehaviours
                .Concat(additionalBehaviors));
            _staticGameObjectBehaviorsInterceptorFacade.Intercept(allBehaviors);

            var staticGameObject = new StaticGameObject(allBehaviors);
            _behaviorManager.Register(staticGameObject, allBehaviors);
            return staticGameObject;
        }
    }
}
