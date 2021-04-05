
using ProjectXyz.Api.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Skills.Api
{
    public interface IInflictDamageBehavior : IBehavior
    {
        // FIXME: we probably actually want something like
        // our filter context here... how can we serialize this?
        //Predicate<IGameObject> TargetFilter { get; }
    }
}