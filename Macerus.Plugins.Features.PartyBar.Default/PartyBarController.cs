using System;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.Camera;
using Macerus.Plugins.Features.Mapping;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Systems;
using ProjectXyz.Plugins.Features.Combat.Api;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Plugins.Features.PartyManagement;

namespace Macerus.Plugins.Features.PartyBar.Default
{
    public sealed class PartyBarController : IPartyBarController
    {
        private readonly Lazy<IRosterManager> _lazyRosterManager;
        private readonly IPartyBarViewModel _partyBarViewModel;
        private readonly IGameObjectToPartyBarPortraitConverter _gameObjectToPartyBarPortraitConverter;
        private readonly Lazy<ICameraManager> _lazyCameraManager;
        private readonly Lazy<ICombatTurnManager> _lazyCombatTurnManager;
        private readonly Lazy<IMappingAmenity> _lazyMappingAmenity;

        public PartyBarController(
            IPartyBarViewModel combatTurnOrderViewModel,
            IGameObjectToPartyBarPortraitConverter gameObjectToPartyBarPortraitConverter,
            Lazy<IRosterManager> lazyRosterManager,
            Lazy<ICameraManager> lazyCameraManager,
            Lazy<ICombatTurnManager> lazyCombatTurnManager,
            Lazy<IMappingAmenity> lazyMappingAmenity)
        {
            _partyBarViewModel = combatTurnOrderViewModel;
            _gameObjectToPartyBarPortraitConverter = gameObjectToPartyBarPortraitConverter;
            _lazyRosterManager = lazyRosterManager;
            _lazyCameraManager = lazyCameraManager;
            _lazyCombatTurnManager = lazyCombatTurnManager;
            _lazyMappingAmenity = lazyMappingAmenity;

            // FIXME: defeats the point of Lazy<T> to do this here so consider
            // not using that...
            _lazyRosterManager.Value.ActivePartyChanged += RosterManager_ActivePartyChanged;
            _lazyRosterManager.Value.PartyLeaderChanged += RosterManager_PartyLeaderChanged;
            _lazyRosterManager.Value.ControlledActorChanged += RosterManager_ControlledActorChanged;

            RefreshPortraits();
        }

        public void ShowPartyBar() => _partyBarViewModel.Open();

        public void ClosePartyBar() => _partyBarViewModel.Close();

        public async Task UpdateAsync(ISystemUpdateContext systemUpdateContext)
        {
            // no-op
            return;
        }

        private void RefreshPortraits()
        {
            foreach (var portrait in _partyBarViewModel.Portraits)
            {
                portrait.Activated -= Portrait_Activated;
            }

            var portraits = _lazyRosterManager
                .Value
                .ActiveParty
                .Select(_gameObjectToPartyBarPortraitConverter.Convert);
            _partyBarViewModel.UpdatePortraits(portraits);

            foreach (var portrait in _partyBarViewModel.Portraits)
            {
                portrait.Activated += Portrait_Activated;
            }
        }

        private void Portrait_Activated(
            object sender,
            EventArgs e)
        {
            var portrait = (IPartyBarPortraitViewModel)sender;
            var actor = _lazyRosterManager
                .Value
                .ActiveParty
                .First(x => Equals(
                    x.GetOnly<IIdentifierBehavior>().Id,
                    portrait.ActorIdentifier));

            // different behavior for the user if we're in combat or not...
            // game logic is not to activate actors out of order
            if (_lazyCombatTurnManager.Value.InCombat)
            {
                _lazyCameraManager.Value.SetFollowTarget(actor);
            }
            else if (!Equals(actor, _lazyRosterManager.Value.ActivePartyLeader))
            {
                var currentLeader = _lazyRosterManager.Value.ActivePartyLeader;

                var currentLeaderPosition = currentLeader.GetOnly<IReadOnlyPositionBehavior>();
                var newLeaderPosition = actor.GetOnly<IPositionBehavior>();
                newLeaderPosition.SetPosition(currentLeaderPosition.X, currentLeaderPosition.Y);

                var currentLeaderMovement = currentLeader.GetOnly<IMovementBehavior>();
                currentLeaderMovement.SetWalkPath(Enumerable.Empty<Vector2>());
                currentLeaderMovement.SetVelocity(0, 0);
                currentLeaderMovement.SetThrottle(0, 0);

                var newLeaderMovement = actor.GetOnly<IMovementBehavior>();
                newLeaderMovement.SetWalkPath(Enumerable.Empty<Vector2>());
                newLeaderMovement.SetVelocity(0, 0);
                newLeaderMovement.SetThrottle(0, 0);
                newLeaderMovement.Direction = currentLeaderMovement.Direction;

                _lazyMappingAmenity.Value.MarkForAddition(actor);
                _lazyMappingAmenity.Value.MarkForRemoval(currentLeader);

                actor.GetOnly<IRosterBehavior>().IsPartyLeader = true;
                actor.GetOnly<IPlayerControlledBehavior>().IsActive = true;
            }
        }

        private void RosterManager_ControlledActorChanged(
            object sender,
            EventArgs e)
        {
            RefreshPortraits();
        }

        private void RosterManager_PartyLeaderChanged(
            object sender,
            EventArgs e)
        {
            RefreshPortraits();
        }

        private void RosterManager_ActivePartyChanged(
            object sender, 
            EventArgs e)
        {
            RefreshPortraits();
        }
    }
}
