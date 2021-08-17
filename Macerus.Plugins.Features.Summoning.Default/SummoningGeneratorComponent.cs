using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;

namespace Macerus.Plugins.Features.Summoning.Default
{
    public sealed class SummoningGeneratorComponent : IGeneratorComponent
    {
        public SummoningGeneratorComponent(IIdentifier summonEnchantmentDefinitionId)
        {
            SummonEnchantmentDefinitionId = summonEnchantmentDefinitionId;
        }

        public IIdentifier SummonEnchantmentDefinitionId { get; }
    }
}
