using System.Linq;

using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation.DropTables;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.GameObjects.Generation;
using ProjectXyz.Shared.Game.GameObjects.Generation.Attributes;

using Xunit;

namespace Macerus.Tests
{
    public sealed class LootGeneratorTests
    {
        private static readonly MacerusContainer _container;

        static LootGeneratorTests()
        {
            _container = new MacerusContainer();
        }

        [Fact]
        public void WIP_TEST()
        {
            var lootGenerator = _container.Resolve<ILootGenerator>();
            var generatorContextProvider = _container.Resolve<IGeneratorContextProvider>();
            var generatorContext = generatorContextProvider.GetGeneratorContext();
            generatorContext = new GeneratorContext(
                1,
                1,
                generatorContext
                    .Attributes
                    .AppendSingle(new GeneratorAttribute(
                        new StringIdentifier("drop-table"),
                        new IdentifierGeneratorAttributeValue(new StringIdentifier("any_magic_1-10_lvl10")),
                        true)));

            var loot = lootGenerator
                .GenerateLoot(generatorContext)
                .ToArray();
        }
    }
}
