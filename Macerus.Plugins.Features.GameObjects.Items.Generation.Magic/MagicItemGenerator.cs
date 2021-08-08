using System.Collections.Generic;
using System.Linq;

using Macerus.Api.Behaviors.Filtering;
using Macerus.Plugins.Features.GameObjects.Items.Behaviors;
using Macerus.Plugins.Features.GameObjects.Items.Generation.Default;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.Filtering.Default.Attributes; // FIXME: dependency on non-API
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Magic
{
    public sealed class MagicItemGenerator : IDiscoverableItemGenerator
    {
        private readonly IFilterAttribute _requiresMagicAffix;
        private readonly IBaseItemGenerator _baseItemGenerator;
        private readonly IFilterContextAmenity _filterContextAmenity;
        private readonly IGameObjectFactory _gameObjectFactory;
        private readonly IGeneratorComponentToBehaviorConverterFacade _generatorComponentToBehaviorConverterFacade;

        public MagicItemGenerator(
            IBaseItemGenerator baseItemGenerator,
            IFilterContextAmenity filterContextAmenity,
            IGameObjectFactory gameObjectFactory,
            IGeneratorComponentToBehaviorConverterFacade generatorComponentToBehaviorConverterFacade)
        {
            _baseItemGenerator = baseItemGenerator;
            _filterContextAmenity = filterContextAmenity;
            _gameObjectFactory = gameObjectFactory;
            _generatorComponentToBehaviorConverterFacade = generatorComponentToBehaviorConverterFacade;

            _requiresMagicAffix = new FilterAttribute(
                new StringIdentifier("affix-type"),
                new StringFilterAttributeValue("magic"),
                true);
            SupportedAttributes = new IFilterAttribute[]
            {
                _requiresMagicAffix,
            };
        }
        
        public IEnumerable<IFilterAttribute> SupportedAttributes { get; } 

        public IEnumerable<IGameObject> GenerateItems(IFilterContext filterContext)
        {
            var magicItemGeneratorContext = _filterContextAmenity.CreateSubContext(
                filterContext,
                SupportedAttributes);
            var baseItems = _baseItemGenerator.GenerateItems(magicItemGeneratorContext);

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
                    new HasInventoryBackgroundColor(0, 0, 255),
                    new HasAffixType(new StringIdentifier("magic")),
                };

                var baseItemBehaviorsToUse = baseItem
                    .Behaviors
                    .Where(x => !(x is IHasReadOnlyEnchantmentsBehavior));
                var magicItemBehaviorsPreGeneration = baseItemBehaviorsToUse
                    .Concat(additionalStaticBehaviors)
                    .ToArray();
                var magicItemBehaviorsPostGeneration = _generatorComponentToBehaviorConverterFacade
                    .Convert(
                        magicItemGeneratorContext,
                        magicItemBehaviorsPreGeneration,
                        new IGeneratorComponent[]
                        {
                            new RandomEnchantmentsGeneratorComponent(
                                1,
                                2,
                                SupportedAttributes),
                            new MagicItemNameGeneratorComponent(),
                        })
                    .ToArray();
                var magicItem = _gameObjectFactory.Create(magicItemBehaviorsPostGeneration);
                yield return magicItem;
            }
        }        
    }
}