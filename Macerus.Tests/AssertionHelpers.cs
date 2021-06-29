using System.Linq;

using Macerus.Plugins.Features.GameObjects.Actors;
using Macerus.Plugins.Features.GameObjects.Items.Behaviors;

using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.Socketing.Api;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api;

using Xunit;

namespace Macerus.Tests
{
    public class AssertionHelpers
    {
        private readonly MacerusContainer _macerusContainer;
        private readonly IStatCalculationService _statCalculationService;
        private readonly IStatCalculationContextFactory _statCalculationContextFactory;
        private readonly IDynamicAnimationIdentifiers _dynamicAnimationIdentifiers;

        public AssertionHelpers(MacerusContainer macerusContainer)
        {
            _macerusContainer = macerusContainer;
            _statCalculationService = _macerusContainer.Resolve<IStatCalculationService>();
            _statCalculationContextFactory = _macerusContainer.Resolve<IStatCalculationContextFactory>();
            _dynamicAnimationIdentifiers = _macerusContainer.Resolve<IDynamicAnimationIdentifiers>();
        }

        public void AssertActorRequirements(IGameObject gameObject)
        {
            var hasStatsBehaviors = gameObject
                .Get<IHasStatsBehavior>()
                .ToArray();
            Assert.True(
                1 == hasStatsBehaviors.Length,
                $"Expected '{gameObject}' to have a single " +
                $"'{typeof(IHasStatsBehavior)}' but had " +
                $"{hasStatsBehaviors.Length}.");

            AssertBaseStatPresent(
                gameObject,
                _dynamicAnimationIdentifiers.AnimationOverrideStatId);
            AssertBaseStatValue(
                gameObject,
                _dynamicAnimationIdentifiers.RedMultiplierStatId,
                1);
            AssertBaseStatValue(
                gameObject,
                _dynamicAnimationIdentifiers.GreenMultiplierStatId,
                1);
            AssertBaseStatValue(
                gameObject,
                _dynamicAnimationIdentifiers.BlueMultiplierStatId,
                1);
            AssertBaseStatValue(
                gameObject,
                _dynamicAnimationIdentifiers.AlphaMultiplierStatId,
                1);
            AssertBaseStatValue(
                gameObject,
                _dynamicAnimationIdentifiers.AnimationSpeedMultiplierStatId,
                1);
        }

        public void AssertBaseStatPresent(
            IGameObject gameObject,
            IIdentifier statDefinitionId)
        {
            var hasStatsBehavior = gameObject.GetOnly<IHasStatsBehavior>();
            Assert.True(
                hasStatsBehavior.BaseStats.ContainsKey(statDefinitionId),
                $"Expected base stat '{statDefinitionId}' of object '{gameObject}' " +
                $"to be present, but it was not.");
        }

        public void AssertBaseStatValue(
            IGameObject gameObject,
            IIdentifier statDefinitionId,
            double expectedValue)
        {
            AssertBaseStatPresent(gameObject, statDefinitionId);
            var hasStatsBehavior = gameObject.GetOnly<IHasStatsBehavior>();
            var actualValue = hasStatsBehavior.BaseStats[statDefinitionId];
            Assert.True(
                expectedValue == actualValue,
                $"Expected base stat '{statDefinitionId}' of object '{gameObject}' " +
                $"to be {expectedValue}, but it was {actualValue}.");
        }

        public void AssertStatValue(
            IGameObject gameObject,
            IIdentifier statDefinitionId,
            double expectedValue)
        {
            var context = _statCalculationContextFactory.Create(
                Enumerable.Empty<IComponent>(),
                Enumerable.Empty<IGameObject>());
            var actualStatValue = _statCalculationService.GetStatValue(
                gameObject,
                statDefinitionId,
                context);
            Assert.True(
                actualStatValue == expectedValue,
                $"Expected stat '{statDefinitionId}' of object '{gameObject}' " +
                $"to be {expectedValue} but was {actualStatValue}.");
        }

        public void AssertSocketBehaviors(IGameObject item)
        {
            var canBeSocketedBehaviors = item
                .Get<ICanBeSocketedBehavior>()
                .ToArray();
            Assert.InRange(canBeSocketedBehaviors.Length, 0, 1);
            if (canBeSocketedBehaviors.Length > 0)
            {
                Assert.InRange(
                    canBeSocketedBehaviors.Single().TotalSockets.Sum(x => x.Value),
                    1,
                    8); // current max limit we're interested in
                Assert.Equal(
                    canBeSocketedBehaviors.Single().TotalSockets.Sum(x => x.Value),
                    canBeSocketedBehaviors.Single().AvailableSockets.Sum(x => x.Value));
                Assert.Equal(0, canBeSocketedBehaviors.Single().OccupiedSockets.Count);
                Assert.Single(item.Get<IApplySocketEnchantmentsBehavior>());
            }
        }

        public void AssertAffix(IGameObject item, IIdentifier requiredAffixId)
        {
            var affixTypeBehaviors = item
                .Get<IHasAffixType>()
                .ToArray();
            Assert.True(
                affixTypeBehaviors.Length == 1,
                $"We expect all items to have exactly 1 affix type. This " +
                $"item had {affixTypeBehaviors.Length} " +
                $"{typeof(IHasAffixType)} behaviors.");
            Assert.Equal(affixTypeBehaviors.Single().AffixTypeId, requiredAffixId);
        }
    }
}
