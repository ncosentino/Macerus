using Macerus.Api.Behaviors;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Shared.Behaviors
{
    public sealed class WorldLocationBehavior :
        BaseBehavior,
        IWorldLocationBehavior
    {
        public double X { get; set; }

        public double Y { get; set; }
    }
}
