using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.GameObjects.Actors.Interactions
{
    public interface IActorActionCheck
    {
        bool CanAct(IGameObject actor);
    }
}
