using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.Summoning.Default
{
    public sealed class SummoningSkillEffectBehavior : 
        BaseBehavior,
        ISummoningSkillEffectBehavior
    {
        public SummoningSkillEffectBehavior(IIdentifier summonEnchantmentDefinitionId)
        {
            SummonEnchantmentDefinitionId = summonEnchantmentDefinitionId;
        }

        public IIdentifier SummonEnchantmentDefinitionId { get; }
    }
}
