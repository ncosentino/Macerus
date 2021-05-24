using System.Linq;

using Macerus.Api.Behaviors;
using Macerus.Api.GameObjects;
using Macerus.Shared.Behaviors;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Framework;

using Xunit;

namespace Macerus.Tests.GameObjects
{
    public sealed class GameObjectRepositoryAmenityTests
    {
        private static readonly MacerusContainer _container;
        private static readonly IGameObjectRepositoryAmenity _gameObjectRepositoryAmenity;

        static GameObjectRepositoryAmenityTests()
        {
            _container = new MacerusContainer();
            _gameObjectRepositoryAmenity = _container.Resolve<IGameObjectRepositoryAmenity>();
        }

        [Fact]
        private void CreateGameObjectFromTemplate_StaticWallNoChanges_BaseBehaviors()
        {
            var gameObject = _gameObjectRepositoryAmenity.CreateGameObjectFromTemplate(
                new StringIdentifier("static/wall"),
                new IBehavior[0]);
            Assert.NotNull(gameObject);
            Assert.Equal(5, gameObject.Behaviors.Count);
            Assert.True(
                gameObject.Behaviors.Single(x =>
                    x is IReadOnlyPrefabResourceIdBehavior &&
                    ((IReadOnlyPrefabResourceIdBehavior)x).PrefabResourceId.Equals(new StringIdentifier("Mapping/Prefabs/static/wall"))) != null,
                $"Expecting a matching '{typeof(IReadOnlyPrefabResourceIdBehavior)}' on '{gameObject}'.");
            Assert.True(
                gameObject.Behaviors.Single(x =>
                    x is IBoxColliderBehavior &&
                    ((IBoxColliderBehavior)x).IsTrigger == false) != null,
                $"Expecting a matching '{typeof(IBoxColliderBehavior)}' on '{gameObject}'.");
            Assert.True(
                gameObject.Behaviors.Single(x =>
                    x is ICreatedFromTemplateBehavior &&
                    ((ICreatedFromTemplateBehavior)x).TemplateId.Equals(new StringIdentifier("static/wall"))) != null,
                $"Expecting a matching '{typeof(ICreatedFromTemplateBehavior)}' on '{gameObject}'.");
            Assert.True(
                gameObject.Behaviors.Single(x =>
                    x is IReadOnlyTypeIdentifierBehavior &&
                    ((IReadOnlyTypeIdentifierBehavior)x).TypeId.Equals(new StringIdentifier("static"))) != null,
                $"Expecting a matching '{typeof(IReadOnlyTypeIdentifierBehavior)}' on '{gameObject}'.");
            Assert.True(
                gameObject.Behaviors.Single(x =>
                    x is IReadOnlyIdentifierBehavior &&
                    ((IReadOnlyIdentifierBehavior)x).Id != null) != null,
                $"Expecting a matching '{typeof(IReadOnlyIdentifierBehavior)}' on '{gameObject}'.");
        }

        [Fact]
        private void CreateGameObjectFromTemplate_StaticWallWithChanges_OverridenBehavior()
        {
            var expectedBoxColliderBehavior = new BoxColliderBehavior(10, 11, 20, 30, false);
            var expectedSizeBehavior = new SizeBehavior(20, 30);
            var gameObject = _gameObjectRepositoryAmenity.CreateGameObjectFromTemplate(
                new StringIdentifier("static/wall"),
                new IBehavior[]
                {
                    expectedBoxColliderBehavior,
                    expectedSizeBehavior,
                });
            Assert.NotNull(gameObject);
            Assert.Equal(6, gameObject.Behaviors.Count);
            Assert.True(
                gameObject.Behaviors.Single(x =>
                    x is IReadOnlyPrefabResourceIdBehavior &&
                    ((IReadOnlyPrefabResourceIdBehavior)x).PrefabResourceId.Equals(new StringIdentifier("Mapping/Prefabs/static/wall"))) != null,
                $"Expecting a matching '{typeof(IReadOnlyPrefabResourceIdBehavior)}' on '{gameObject}'.");
            Assert.True(
                gameObject.Behaviors.Single(x =>
                    x is ICreatedFromTemplateBehavior &&
                    ((ICreatedFromTemplateBehavior)x).TemplateId.Equals(new StringIdentifier("static/wall"))) != null,
                $"Expecting a matching '{typeof(ICreatedFromTemplateBehavior)}' on '{gameObject}'.");
            Assert.True(
                gameObject.Behaviors.Single(x =>
                    x is IReadOnlyTypeIdentifierBehavior &&
                    ((IReadOnlyTypeIdentifierBehavior)x).TypeId.Equals(new StringIdentifier("static"))) != null,
                $"Expecting a matching '{typeof(IReadOnlyTypeIdentifierBehavior)}' on '{gameObject}'.");
            Assert.True(
                gameObject.Behaviors.Single(x =>
                    x is IReadOnlyIdentifierBehavior &&
                    ((IReadOnlyIdentifierBehavior)x).Id != null) != null,
                $"Expecting a matching '{typeof(IReadOnlyIdentifierBehavior)}' on '{gameObject}'.");

            var boxColliderBehavior = Assert.Single(gameObject.Behaviors.TakeTypes<IBoxColliderBehavior>());
            Assert.Equal(expectedBoxColliderBehavior, boxColliderBehavior);
            var sizeBehavior = Assert.Single(gameObject.Behaviors.TakeTypes<ISizeBehavior>());
            Assert.Equal(expectedSizeBehavior, sizeBehavior);
        }
    }
}
