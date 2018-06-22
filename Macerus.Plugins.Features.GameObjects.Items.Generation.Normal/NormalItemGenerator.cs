using System.Collections.Generic;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Api.GameObjects.Items.Generation;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.GameObjects.Generation.Attributes;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Normal
{
    public sealed class NormalItemGenerator : IItemGenerator
    {
        private readonly IBaseItemGenerator _baseItemGenerator;

        public NormalItemGenerator(IBaseItemGenerator baseItemGenerator)
        {
            _baseItemGenerator = baseItemGenerator;
        }

        public IEnumerable<IGameObject> GenerateItems(IGeneratorContext generatorContext)
        {
            var items = _baseItemGenerator.GenerateItems(generatorContext);
            return items;
        }

        public IEnumerable<IGeneratorAttribute> SupportedAttributes { get; } = new IGeneratorAttribute[]
        {
            new GeneratorAttribute(new StringIdentifier("affix-type"), new StringGeneratorAttributeValue("normal")),
        };
    }
}
