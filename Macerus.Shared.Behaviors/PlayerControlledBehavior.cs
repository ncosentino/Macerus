using Macerus.Api.Behaviors;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Shared.Behaviors
{
    public class PlayerControlledBehavior :
        BaseBehavior,
        IPlayerControlledBehavior
    {
        public bool IsActive { get; set; }
    }
}