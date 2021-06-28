
using System;

namespace Macerus.Plugins.Features.Camera
{
    public interface IObservableCameraManager : IReadOnlyCameraManager
    {
        event EventHandler<EventArgs> FollowTargetChanged;
    }
}
