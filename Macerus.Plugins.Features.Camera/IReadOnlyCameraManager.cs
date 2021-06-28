using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.Camera
{
    public interface IReadOnlyCameraManager
    {
        IGameObject FollowTarget { get; }
    }
}
