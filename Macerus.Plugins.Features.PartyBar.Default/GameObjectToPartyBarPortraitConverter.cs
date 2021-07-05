using Macerus.Plugins.Features.Gui.Default;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.PartyManagement;

namespace Macerus.Plugins.Features.PartyBar.Default
{
    public sealed class GameObjectToPartyBarPortraitConverter : IGameObjectToPartyBarPortraitConverter
    {
        private readonly IReadOnlyRosterManager _rosterManager;

        public GameObjectToPartyBarPortraitConverter(IReadOnlyRosterManager rosterManager)
        {
            _rosterManager = rosterManager;
        }

        public IPartyBarPortraitViewModel Convert(IGameObject actor)
        {
            var actorId = actor.GetOnly<IIdentifierBehavior>().Id;
            var isLeader = Equals(actor, _rosterManager.ActivePartyLeader);
            var borderColor = isLeader
                ? new Color() { R = 0, G = 128, B = 0, A = 0x80 }
                : new Color() { R = 0, G = 0, B = 128, A = 0x80 };
            var backgroundColor = isLeader
                ? new Color() { R = 0, G = 128, B = 0, A = 0x40 }
                : new Color() { R = 0, G = 0, B = 128, A = 0x40 };
            var actorName = actor
                .GetOnly<IHasDisplayNameBehavior>()
                .DisplayName;
            var iconResourceId = actor
                .GetOnly<IHasDisplayIconBehavior>()
                .IconResourceId;
            var portraitViewModel = new PartyBarPortraitViewModel(
                actorId,
                borderColor,
                backgroundColor,
                actorName,
                iconResourceId);
            return portraitViewModel;
        }
    }
}
