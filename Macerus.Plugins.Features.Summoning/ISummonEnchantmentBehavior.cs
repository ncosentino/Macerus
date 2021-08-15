using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace Macerus.Plugins.Features.Summoning
{
    public interface ISummonEnchantmentBehavior : IBehavior
    {
        IGameObject SummonDefinition { get; }
    }
}
