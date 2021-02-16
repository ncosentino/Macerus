using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.GameObjects.Items.Behaviors;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.GameObjects.Generation;
using ProjectXyz.Shared.Game.GameObjects.Generation.Attributes;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Normal
{
    public sealed class NormalItemGenerator : IDiscoverableItemGenerator
    {
        private readonly IBaseItemGenerator _baseItemGenerator;
        private readonly IItemFactory _itemFactory;

        public NormalItemGenerator(
            IBaseItemGenerator baseItemGenerator, IItemFactory itemFactory)
        {
            _baseItemGenerator = baseItemGenerator;
            _itemFactory = itemFactory;
        }

        public IEnumerable<IGameObject> GenerateItems(IGeneratorContext generatorContext)
        {
            var normalGeneratorContext = new GeneratorContext(
                generatorContext.MinimumGenerateCount,
                generatorContext.MaximumGenerateCount,
                generatorContext
                    .Attributes
                    .Where(x => !SupportedAttributes.Any(s => s.Id.Equals(x.Id)))
                    .Concat(SupportedAttributes));
            var baseItems = _baseItemGenerator.GenerateItems(normalGeneratorContext);
            var items = baseItems.Select(baseItem => _itemFactory.Create(baseItem.Behaviors.Concat(new[]
            {
                new HasInventoryDisplayColor(255, 255, 255, 255),
            })));
            return items;
        }

        public IEnumerable<IGeneratorAttribute> SupportedAttributes { get; } = new IGeneratorAttribute[]
        {
            new GeneratorAttribute(
                new StringIdentifier("affix-type"),
                new StringGeneratorAttributeValue("normal"),
                true),
        };
    }
}
