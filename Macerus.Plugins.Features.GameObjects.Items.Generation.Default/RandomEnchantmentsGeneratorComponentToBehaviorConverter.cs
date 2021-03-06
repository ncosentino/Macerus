﻿using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Enchantments.Generation;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Filtering.Api;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Default
{
    public sealed class RandomEnchantmentsGeneratorComponentToBehaviorConverter : IDiscoverableGeneratorComponentToBehaviorConverter
    {
        private readonly Lazy<IEnchantmentGenerator> _lazyEnchantmentGenerator;
        private readonly Lazy<IHasEnchantmentsBehaviorFactory> _lazyHasEnchantmentsBehaviorFactory;
        private readonly Lazy<IFilterContextFactory> _lazyFilterContextFactory;

        public RandomEnchantmentsGeneratorComponentToBehaviorConverter(
            Lazy<IEnchantmentGenerator> lazyEnchantmentGenerator,
            Lazy<IHasEnchantmentsBehaviorFactory> lazyHasEnchantmentsBehaviorFactory,
            Lazy<IFilterContextFactory> lazyFilterContextFactory)
        {
            _lazyEnchantmentGenerator = lazyEnchantmentGenerator;
            _lazyHasEnchantmentsBehaviorFactory = lazyHasEnchantmentsBehaviorFactory;
            _lazyFilterContextFactory = lazyFilterContextFactory;
        }

        public Type ComponentType => typeof(RandomEnchantmentsGeneratorComponent);

        private IEnchantmentGenerator EnchantmentGenerator => _lazyEnchantmentGenerator.Value;

        private IHasEnchantmentsBehaviorFactory HasEnchantmentsBehaviorFactory => _lazyHasEnchantmentsBehaviorFactory.Value;

        private IFilterContextFactory FilterContextFactory => _lazyFilterContextFactory.Value;

        public IEnumerable<IBehavior> Convert(
            IFilterContext filterContext,
            IEnumerable<IBehavior> baseBehaviors,
            IGeneratorComponent generatorComponent)
        {
            var randomEnchantmentsGeneratorComponent = (RandomEnchantmentsGeneratorComponent)generatorComponent;

            var wasNewlyCreatedEnchantmentsBehavior = false;
            IHasEnchantmentsBehavior hasEnchantmentsBehavior;
            if ((hasEnchantmentsBehavior = baseBehaviors
                .Get<IHasEnchantmentsBehavior>()
                .SingleOrDefault()) == null)
            {
                hasEnchantmentsBehavior = HasEnchantmentsBehaviorFactory.Create();
                wasNewlyCreatedEnchantmentsBehavior = true;

                IHasReadOnlyEnchantmentsBehavior hasReadOnlyEnchantmentsBehavior;
                if ((hasReadOnlyEnchantmentsBehavior = baseBehaviors
                    .Get<IHasReadOnlyEnchantmentsBehavior>()
                    .SingleOrDefault()) != null)
                {
                    hasEnchantmentsBehavior.AddEnchantments(hasReadOnlyEnchantmentsBehavior.Enchantments);
                }
            }

            var attributes = filterContext
                .Attributes
                .Where(x => !randomEnchantmentsGeneratorComponent.EnchantmentDefinitionFilter.Any(s => s.Id.Equals(x.Id)))
                .Concat(randomEnchantmentsGeneratorComponent.EnchantmentDefinitionFilter);
            var enchantmentGeneratorContext = FilterContextFactory.CreateContext(
                randomEnchantmentsGeneratorComponent.MinimumEnchantments,
                randomEnchantmentsGeneratorComponent.MaximumEnchantments,
                attributes);
            var enchantments = EnchantmentGenerator
                .GenerateEnchantments(enchantmentGeneratorContext)
                .ToArray();
            if (!enchantments.Any())
            {
                throw new InvalidOperationException(
                    $"No enchantments were added to the base item.");
            }

            hasEnchantmentsBehavior.AddEnchantments(enchantments);

            if (wasNewlyCreatedEnchantmentsBehavior)
            {
                yield return hasEnchantmentsBehavior;
            }
        }
    }
}