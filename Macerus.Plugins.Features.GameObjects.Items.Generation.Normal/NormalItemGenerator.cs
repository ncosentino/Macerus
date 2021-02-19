using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.GameObjects.Items.Behaviors;

using ProjectXyz.Api.Behaviors;
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

        public NormalItemGenerator(IBaseItemGenerator baseItemGenerator)
        {
            _baseItemGenerator = baseItemGenerator;
        }

        public IEnumerable<IGameObject> GenerateItems(IGeneratorContext generatorContext)
        {
            // create our new context by keeping information about attributes 
            // from our caller, but acknowledging that any that were required
            // are now fulfilled up until this point. we then cobine in the
            // newly provided attributes from the drop table.
            var generatorRequired = SupportedAttributes
                .Where(attr => attr.Required)
                .ToDictionary(x => x.Id, x => x);
            var normalGeneratorContext = new GeneratorContext(
                generatorContext.MinimumGenerateCount,
                generatorContext.MaximumGenerateCount,
                generatorContext
                    .Attributes
                    .Where(x => !generatorRequired.ContainsKey(x.Id))
                    .Select(x => x.Required
                        ? x.CopyWithRequired(false)
                        : x)
                    .Concat(generatorRequired.Values));
            var baseItems = _baseItemGenerator.GenerateItems(normalGeneratorContext);
            var items = baseItems.Select(baseItem => new NormalItem(baseItem.Behaviors.Concat(new IBehavior[]
            {
                new HasInventoryDisplayColor(255, 255, 255, 255),
                new HasAffixType(new StringIdentifier("normal")),
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
