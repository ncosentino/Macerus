using System;
using System.Threading.Tasks;

using Macerus.Plugins.Features.Camera;

using ProjectXyz.Api.Systems;
using ProjectXyz.Plugins.Features.PartyManagement;

namespace Macerus.Plugins.Features.GameObjects.Actors.Default
{
    public sealed class CameraFollowingSystem : IDiscoverableSystem
    {
        private readonly IObservableRosterManager _rosterManager;
        private readonly Lazy<ICameraManager> _lazyCameraManager;

        public CameraFollowingSystem(
            IObservableRosterManager rosterManager,
            Lazy<ICameraManager> lazyCameraManager)
        {
            _rosterManager = rosterManager;
            _lazyCameraManager = lazyCameraManager;

            _rosterManager.ControlledActorChanged += RosterManager_ControlledActorChanged;
        }

        public int? Priority => null;

        public async Task UpdateAsync(ISystemUpdateContext systemUpdateContext)
        {
        }

        private void RosterManager_ControlledActorChanged(
            object sender,
            EventArgs e)
        {
            _lazyCameraManager.Value.SetFollowTarget(_rosterManager.CurrentlyControlledActor);
        }
    }
}
