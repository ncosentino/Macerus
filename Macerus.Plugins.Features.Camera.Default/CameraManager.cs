using System;

using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.Camera.Default
{
    public sealed class CameraManager : ICameraManager
    {
        public event EventHandler<EventArgs> FollowTargetChanged;

        public IGameObject FollowTarget { get; private set; }

        public void SetFollowTarget(IGameObject target)
        {
            if (FollowTarget == target)
            {
                return;
            }

            FollowTarget = target;
            FollowTargetChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
