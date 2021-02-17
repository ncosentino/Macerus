using System.Linq;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Items.SocketPatterns.Api;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.GameObjects.Generation;
using ProjectXyz.Shared.Game.GameObjects.Generation.Attributes;

namespace Macerus.Plugins.Features.GameObjects.Items.Socketing.SocketPatterns
{
    public sealed class SocketPatternHandler : IDiscoverableSocketPatternHandler
    {
        private readonly IItemGeneratorFacade _itemGenerator;
        private readonly IGeneratorContextProvider _generatorContextProvider;

        public SocketPatternHandler(
            IItemGeneratorFacade itemGenerator,
            IGeneratorContextProvider generatorContextProvider)
        {
            _itemGenerator = itemGenerator;
            _generatorContextProvider = generatorContextProvider;
        }

        public bool TryHandle(
            ISocketableInfo socketableInfo,
            out IGameObject newItem)
        {
            // FIXME: this is just a hack to test things out
            if (socketableInfo.OccupiedSockets.Count == 1)
            {
                var generatorContext = _generatorContextProvider
                    .GetGeneratorContext()
                    .WithAdditionalAttributes(new[]
                    {
                        new GeneratorAttribute(
                            new StringIdentifier("affix-type"),
                            new StringGeneratorAttributeValue("unique"),
                            true)
                    })
                    .WithGenerateCountRange(1, 1);
                newItem = _itemGenerator
                    .GenerateItems(generatorContext)
                    .Single();
                return true;
            }

            newItem = null;
            return false;
        }
    }
}
