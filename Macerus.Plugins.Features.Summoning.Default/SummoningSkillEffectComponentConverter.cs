using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Skills;
using ProjectXyz.Plugins.Features.GameObjects.Skills.Effects;

namespace Macerus.Plugins.Features.Summoning.Default
{
    public sealed class SummoningSkillEffectComponentConverter : ISkillEffectBehaviorConverter
    {
        public bool CanConvert(IGeneratorComponent component)
        {
            return component is SummoningGeneratorComponent;
        }

        public IBehavior ConvertToBehavior(IGeneratorComponent component)
        {
            var summoningGeneratorComponent = (SummoningGeneratorComponent)component;

            return new SummoningSkillEffectBehavior(summoningGeneratorComponent.SummonEnchantmentDefinitionId);
        }
    }
}
