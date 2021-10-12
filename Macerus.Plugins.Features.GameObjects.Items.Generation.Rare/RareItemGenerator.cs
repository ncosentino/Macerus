using System.Collections.Generic;
using System.Linq;

using Macerus.Api.Behaviors.Filtering;
using Macerus.Plugins.Features.GameObjects.Items.Affixes.Default;
using Macerus.Plugins.Features.GameObjects.Items.Behaviors;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.Filtering.Default.Attributes; // FIXME: dependency on non-API
using ProjectXyz.Plugins.Features.GameObjects.Enchantments;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Rare
{
    public sealed class RareItemGenerator : IDiscoverableItemGenerator
    {
        private readonly IFilterAttribute _requiresRareAffix;
        private readonly IBaseItemGenerator _baseItemGenerator;
        private readonly IFilterContextAmenity _filterContextAmenity;
        private readonly IGameObjectFactory _gameObjectFactory;
        private readonly IGeneratorComponentToBehaviorConverterFacade _generatorComponentToBehaviorConverterFacade;

        public RareItemGenerator(
            IBaseItemGenerator baseItemGenerator,
            IFilterContextAmenity filterContextAmenity,
            IGameObjectFactory gameObjectFactory,
            IGeneratorComponentToBehaviorConverterFacade generatorComponentToBehaviorConverterFacade)
        {
            _baseItemGenerator = baseItemGenerator;
            _filterContextAmenity = filterContextAmenity;
            _gameObjectFactory = gameObjectFactory;
            _generatorComponentToBehaviorConverterFacade = generatorComponentToBehaviorConverterFacade;

            _requiresRareAffix = new FilterAttribute(
                new StringIdentifier("affix-type"),
                new StringFilterAttributeValue("rare"),
                true);
            SupportedAttributes = new IFilterAttribute[]
            {
                _requiresRareAffix,
            };
        }
        
        public IEnumerable<IFilterAttribute> SupportedAttributes { get; } 

        public IEnumerable<IGameObject> GenerateItems(IFilterContext filterContext)
        {
            var rareItemGeneratorContext = _filterContextAmenity.CreateSubContext(
                filterContext,
                SupportedAttributes);
            var baseItems = _baseItemGenerator.GenerateItems(rareItemGeneratorContext);

            foreach (var baseItem in baseItems)
            {
                // TODO: we may need to create a NEW context here to add even more specific information.
                // i.e.
                // - original context says items can be "any armor", but we
                //   generate a helm... we might want helm specific enchantments
                // - original context has a range for item level, but if our 
                //   item was at one end of that range, it might mean better or
                //    worse enchantments given the item level.
                
                var additionalStaticBehaviors = new List<IBehavior>()
                {
                    new HasInventoryBackgroundColor(255, 221, 51),
                    new HasAffixType(new StringIdentifier("rare")),
                };

                var baseItemBehaviorsToUse = baseItem
                    .Behaviors
                    .Where(x => !(x is IReadOnlyHasEnchantmentsBehavior));
                var rareItemBehaviorsPreGeneration = baseItemBehaviorsToUse
                    .Concat(additionalStaticBehaviors)
                    .ToArray();
                var rareItemBehaviorsPostGeneration = _generatorComponentToBehaviorConverterFacade
                    .Convert(
                        rareItemGeneratorContext,
                        rareItemBehaviorsPreGeneration,
                        new IGeneratorComponent[]
                        {
                            new RandomAffixGeneratorComponent(
                                3,
                                6,
                                SupportedAttributes),
                            new RareItemNameGeneratorComponent(),
                        })
                    .ToArray();
                var rareItem = _gameObjectFactory.Create(rareItemBehaviorsPostGeneration);
                yield return rareItem;
            }
        }        
    }
}