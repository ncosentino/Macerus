using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.GameObjects.Enchantments;
using Macerus.Plugins.Features.Summoning.Default;

using NexusLabs.Contracts;

using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.BaseStatEnchantments.Enchantments;
using ProjectXyz.Plugins.Features.ExpiringEnchantments;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.Filtering.Default.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.Default.Calculations;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.Generation.Default;
using ProjectXyz.Plugins.Features.GameObjects.Generation.Default;
using ProjectXyz.Plugins.Features.TurnBased.Default.Duration;
using ProjectXyz.Shared.Framework;

namespace Macerus.Content.Enchantments
{
    public sealed class EnchantmentDefinitionBuilder
    {
        private readonly IEnchantmentIdentifiers _enchantmentIdentifiers;
        private readonly ICalculationPriorityFactory _calculationPriorityFactory;
        private readonly IGameObjectFactory _gameObjectFactory;
        private readonly List<Func<IBehavior>> _statefulBehaviors;

        private IEnchantmentDefinition _current;

        public EnchantmentDefinitionBuilder(
            IEnchantmentIdentifiers enchantmentIdentifiers,
            ICalculationPriorityFactory calculationPriorityFactory,
            IGameObjectFactory gameObjectFactory)
        {
            _statefulBehaviors = new List<Func<IBehavior>>();
            _enchantmentIdentifiers = enchantmentIdentifiers;
            _calculationPriorityFactory = calculationPriorityFactory;
            _gameObjectFactory = gameObjectFactory;
            Reset();
        }

        public EnchantmentDefinitionBuilder WithEnchantmentDefinitionId(
            IIdentifier enchantmentDefinitionId)
        {
            var filter = _current.SupportedAttributes.ToList();
            Contract.Requires(
                !filter.Any(x => Equals(x.Id, _enchantmentIdentifiers.EnchantmentDefinitionId)),
                $"There is already a filter for '{_enchantmentIdentifiers.EnchantmentDefinitionId}'.");

            filter.Add(new FilterAttribute(
                _enchantmentIdentifiers.EnchantmentDefinitionId,
                new IdentifierFilterAttributeValue(enchantmentDefinitionId),
                true));

            _current = new EnchantmentDefinition(
                filter,
                _current.GeneratorComponents);

            return this;
        }

        public EnchantmentDefinitionBuilder WithStatDefinitionId(
            IIdentifier statDefinitionId)
        {
            Contract.Requires(
               !_current.GeneratorComponents.Any(x => x is HasStatGeneratorComponent),
               $"There is already a generator component for '{typeof(HasStatGeneratorComponent)}'.");

            _current = new EnchantmentDefinition(
                _current.SupportedAttributes,
                _current.GeneratorComponents.AppendSingle(new HasStatGeneratorComponent(statDefinitionId)));

            return this;
        }

        public EnchantmentDefinitionBuilder ThatAppliesToOwner()
        {
            Contract.Requires(
               !_current.GeneratorComponents.Any(x => x is EnchantmentTargetGeneratorComponent),
               $"There is already a generator component for '{typeof(EnchantmentTargetGeneratorComponent)}'.");

            _current = new EnchantmentDefinition(
                _current.SupportedAttributes,
                _current.GeneratorComponents.AppendSingle(new EnchantmentTargetGeneratorComponent(new StringIdentifier("owner"))));

            return this;
        }

        public EnchantmentDefinitionBuilder ThatsUsedForPassiveSkill()
        {
            Contract.Requires(
               !_current.GeneratorComponents.Any(x => x is EnchantmentTargetGeneratorComponent),
               $"There is already a generator component for '{typeof(EnchantmentTargetGeneratorComponent)}'.");

            _current = new EnchantmentDefinition(
                _current.SupportedAttributes,
                _current.GeneratorComponents.AppendSingle(new EnchantmentTargetGeneratorComponent(new StringIdentifier("owner.owner.owner"))));

            return this;
        }

        public EnchantmentDefinitionBuilder ThatAppliesEffectsToSkillUser()
        {
            Contract.Requires(
               !_current.GeneratorComponents.Any(x => x is EnchantmentTargetGeneratorComponent),
               $"There is already a generator component for '{typeof(EnchantmentTargetGeneratorComponent)}'.");

            _current = new EnchantmentDefinition(
                _current.SupportedAttributes,
                _current.GeneratorComponents.AppendSingle(new EnchantmentTargetGeneratorComponent(new StringIdentifier("owner.owner.owner"))));

            return this;
        }

        public EnchantmentDefinitionBuilder ThatSummons(
            IIdentifier spawnTableId,
            IIdentifier summonLimitStatPairId)
        {
            var builder = WithStatDefinitionId(new StringIdentifier("summon"));

            // FIXME: properly check if we can add these behaviors/components

            _current = new EnchantmentDefinition(
                _current.SupportedAttributes,
                _current.GeneratorComponents.Concat(new IGeneratorComponent[]
                {
                    new StatelessBehaviorGeneratorComponent(
                        new SummonEnchantmentBehavior(_gameObjectFactory.Create(new IBehavior[]
                        {
                            new SummonSpawnTableBehavior(spawnTableId),
                            new SummonLimitStatBehavior(summonLimitStatPairId),
                        }))),
                }));

            return builder;
        }

        public EnchantmentDefinitionBuilder ThatHasValueInRange(
            int minValue,
            int maxValue,
            int decimalPlaces)
        {
            var statGeneratorComponent = (HasStatGeneratorComponent)_current.GeneratorComponents.FirstOrDefault(x => x is HasStatGeneratorComponent);
            Contract.Requires(
               statGeneratorComponent != null,
               $"There is already a generator component for '{typeof(HasStatGeneratorComponent)}'.");
            return ThatHasValueInRange(
                statGeneratorComponent.StatDefinitionId,
                minValue,
                maxValue,
                decimalPlaces);
        }

        public EnchantmentDefinitionBuilder ThatHasValueInRange(
            IIdentifier statDefinitionId,
            int minValue,
            int maxValue,
            int decimalPlaces)
        {
            Contract.Requires(
               !_current.GeneratorComponents.Any(x => x is RandomRangeExpressionGeneratorComponent),
               $"There is already a generator component for '{typeof(RandomRangeExpressionGeneratorComponent)}'.");

            WithStatDefinitionId(statDefinitionId);

            _current = new EnchantmentDefinition(
                _current.SupportedAttributes,
                _current.GeneratorComponents.AppendSingle(new RandomRangeExpressionGeneratorComponent(
                    statDefinitionId,
                    "+",
                    _calculationPriorityFactory.Create<int>(1),
                    minValue,
                    maxValue,
                    decimalPlaces)));

            return this;
        }

        public EnchantmentDefinitionBuilder ThatModifiesBaseStatWithExpression(string expression)
        {
            _statefulBehaviors.Add(() => new EnchantmentExpressionBehavior(
                _calculationPriorityFactory.Create<int>(1),
                expression));
            _statefulBehaviors.Add(() => new AppliesToBaseStat());

            return this;
        }

        public EnchantmentDefinitionBuilder ThatExpiresAfterTurns(int turns)
        {
            _statefulBehaviors.Add(() => new ExpiryTriggerBehavior(new DurationInTurnsTriggerBehavior(turns)));
            return this;
        }

        public EnchantmentDefinitionBuilder ThatAppliesInstantlyForAttack()
        {
            _statefulBehaviors.Add(() => new ExpiryTriggerBehavior(new DurationInActionsTriggerBehavior(1)));
            return this;
        }

        public EnchantmentDefinitionBuilder ThatAppliesInstantlyAsSingleUse()
        {
            _statefulBehaviors.Add(() => new ExpiryTriggerBehavior(new DurationInActionsTriggerBehavior(1)));
            return this;
        }

        public IEnchantmentDefinition Build()
        {
            Contract.Requires(
                _current.SupportedAttributes.Any(x => Equals(x.Id, _enchantmentIdentifiers.EnchantmentDefinitionId)),
                $"There is no filter for '{_enchantmentIdentifiers.EnchantmentDefinitionId}'. " +
                $"Consider using '{nameof(WithEnchantmentDefinitionId)}'.");
            Contract.Requires(
               _current.GeneratorComponents.Any(x => x is EnchantmentTargetGeneratorComponent),
               $"There is no generator component for '{typeof(EnchantmentTargetGeneratorComponent)}'. " +
               $"Consider using '{nameof(ThatAppliesToOwner)}'.");

            if (_statefulBehaviors.Any())
            {
                var statefulCopy = _statefulBehaviors.ToArray();
                Func<IReadOnlyCollection<IBehavior>> statefulGenerator = () => statefulCopy
                    .Select(x => x())
                    .ToReadOnlyCollection();
                _current = new EnchantmentDefinition(
                    _current.SupportedAttributes,
                    _current.GeneratorComponents.AppendSingle(new StatefulBehaviorGeneratorComponent(statefulGenerator)));
            }

            var built = _current;
            Reset();
            return built;
        }

        private void Reset()
        {
            _statefulBehaviors.Clear();
            _current = new EnchantmentDefinition(
                Enumerable.Empty<IFilterAttribute>(),
                Enumerable.Empty<IGeneratorComponent>());
        }
    }
}
