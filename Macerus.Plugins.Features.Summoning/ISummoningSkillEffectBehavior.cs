using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace Macerus.Plugins.Features.Summoning
{
    public interface ISummoningSkillEffectBehavior : IBehavior
    {
        IIdentifier SummonEnchantmentDefinitionId { get; }
    }
}
