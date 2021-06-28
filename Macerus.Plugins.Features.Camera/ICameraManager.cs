using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.Camera
{
    public interface ICameraManager : IObservableCameraManager
    {
        void SetFollowTarget(IGameObject target);
    }
}
