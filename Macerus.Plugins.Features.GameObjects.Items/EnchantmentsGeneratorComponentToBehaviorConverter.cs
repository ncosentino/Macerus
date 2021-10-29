using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Api.Behaviors.Filtering;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Default.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.Generation;

namespace Macerus.Plugins.Features.GameObjects.Items
{
    public sealed class EnchantmentsGeneratorComponentToBehaviorConverter : IDiscoverableGeneratorComponentToBehaviorConverter
    {
        private readonly Lazy<IEnchantmentGenerator> _lazyEnchantmentGenerator;
        private readonly Lazy<IHasEnchantmentsBehaviorFactory> _lazyHasEnchantmentsBehaviorFactory;
        private readonly Lazy<IFilterContextAmenity> _lazyFilterContextAmenity;

        public EnchantmentsGeneratorComponentToBehaviorConverter(
            Lazy<IEnchantmentGenerator> lazyEnchantmentGenerator,
            Lazy<IHasEnchantmentsBehaviorFactory> lazyHasEnchantmentsBehaviorFactory,
            Lazy<IFilterContextAmenity> lazyFilterContextAmenity)
        {
            _lazyEnchantmentGenerator = lazyEnchantmentGenerator;
            _lazyHasEnchantmentsBehaviorFactory = lazyHasEnchantmentsBehaviorFactory;
            _lazyFilterContextAmenity = lazyFilterContextAmenity;
        }

        public Type ComponentType => typeof(EnchantmentsGeneratorComponent);

        public IEnumerable<IBehavior> Convert(
            IFilterContext filterContext,
            IEnumerable<IBehavior> baseBehaviors,
            IGeneratorComponent generatorComponent)
        {
            var enchantmentsGeneratorComponent = (EnchantmentsGeneratorComponent)generatorComponent;

            var wasNewlyCreatedEnchantmentsBehavior = false;
            IHasEnchantmentsBehavior hasEnchantmentsBehavior;
            if ((hasEnchantmentsBehavior = baseBehaviors
                .Get<IHasEnchantmentsBehavior>()
                .SingleOrDefault()) == null)
            {
                hasEnchantmentsBehavior = _lazyHasEnchantmentsBehaviorFactory.Value.Create();
                wasNewlyCreatedEnchantmentsBehavior = true;

                IReadOnlyHasEnchantmentsBehavior hasReadOnlyEnchantmentsBehavior;
                if ((hasReadOnlyEnchantmentsBehavior = baseBehaviors
                    .Get<IReadOnlyHasEnchantmentsBehavior>()
                    .SingleOrDefault()) != null)
                {
                    hasEnchantmentsBehavior
                        .AddEnchantmentsAsync(hasReadOnlyEnchantmentsBehavior.Enchantments)
                        .Wait();
                }
            }

            var enchantments = GenerateEnchantments(
                filterContext,
                enchantmentsGeneratorComponent);
            hasEnchantmentsBehavior
                .AddEnchantmentsAsync(enchantments)
                .Wait();

            if (wasNewlyCreatedEnchantmentsBehavior)
            {
                yield return hasEnchantmentsBehavior;
            }
        }

        private IEnumerable<IGameObject> GenerateEnchantments(
            IFilterContext filterContext,
            EnchantmentsGeneratorComponent enchantmentsGeneratorComponent)
        {
            bool generatedAny = false;
            foreach (var enchantmentDefinitionFilter in enchantmentsGeneratorComponent.FiltersForEachEnchantmentDefinition)
            {
                var attributes = filterContext
                    .Attributes
                    .Where(x => !enchantmentDefinitionFilter.Any(s => s.Id.Equals(x.Id)))
                    .Select(x => x.CopyWithRequired(false))
                    .Concat(enchantmentDefinitionFilter);
                var enchantmentGeneratorContext = _lazyFilterContextAmenity.Value.CreateFilterContextForSingle(attributes);
                foreach (var enchantment in _lazyEnchantmentGenerator.Value.GenerateEnchantments(enchantmentGeneratorContext))
                {
                    generatedAny = true;
                    yield return enchantment;
                }                
            }

            if (!generatedAny)
            {
                throw new InvalidOperationException(
                    $"No enchantments were generated for the specified context" +
                    $". Please consider debugging to see if the context is invalid.");
            }
        }
    }
}