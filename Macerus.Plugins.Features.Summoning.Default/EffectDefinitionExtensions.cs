using System.Linq;
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.GameObjects.Skills.Effects;

namespace Macerus.Plugins.Features.Summoning.Default
{
    public static class EffectDefinitionExtensions
    {
        public static ISkillEffectDefinition Summons(
            this ISkillEffectDefinition oldSkillDefinition,
            IIdentifier summonEnchantmentDefinitionId)
        {
            var summoningGeneratorComponent = new SummoningGeneratorComponent(summonEnchantmentDefinitionId);

            var skillDefinition = new SkillEffectDefinition(
                oldSkillDefinition.SupportedAttributes,
                oldSkillDefinition
                    .FilterComponents
                    .AppendSingle(summoningGeneratorComponent));

            return skillDefinition;
        }
    }
}
