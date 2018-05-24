using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.GameObjects.Actors
{
    public interface IActorRepository
    {
        IGameObject Load(IIdentifier objectId);
    }
}