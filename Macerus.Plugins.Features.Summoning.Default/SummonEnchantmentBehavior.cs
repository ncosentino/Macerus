using ProjectXyz.Api.GameObjects;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.Summoning.Default
{
    public sealed class SummonEnchantmentBehavior :
        BaseBehavior,
        ISummonEnchantmentBehavior
    {
        public SummonEnchantmentBehavior(IGameObject summonDefinition)
        {
            SummonDefinition = summonDefinition;
        }

        public IGameObject SummonDefinition { get; }
    }
}
