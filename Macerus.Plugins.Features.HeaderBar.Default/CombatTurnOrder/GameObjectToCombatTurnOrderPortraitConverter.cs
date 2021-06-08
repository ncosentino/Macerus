using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.Combat.Api;
using Macerus.Plugins.Features.Gui.Default;
using Macerus.Plugins.Features.HeaderBar.Api.CombatTurnOrder;
using Macerus.Plugins.Features.Stats.Api;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.HeaderBar.Default.CombatTurnOrder
{
    public sealed class GameObjectToCombatTurnOrderPortraitConverter : IGameObjectToCombatTurnOrderPortraitConverter
    {
        private readonly IStatCalculationServiceAmenity _statCalculationServiceAmenity;
        private readonly ICombatTeamIdentifiers _combatTeamIdentifiers;

        public GameObjectToCombatTurnOrderPortraitConverter(
            IStatCalculationServiceAmenity statCalculationServiceAmenity,
            ICombatTeamIdentifiers combatTeamIdentifiers)
        {
            _statCalculationServiceAmenity = statCalculationServiceAmenity;
            _combatTeamIdentifiers = combatTeamIdentifiers;
        }

        public ICombatTurnOrderPortraitViewModel Convert(IGameObject actor)
        {
            var actorId = actor.GetOnly<IIdentifierBehavior>().Id;
            var team = _statCalculationServiceAmenity.GetStatValue(
                actor,
                _combatTeamIdentifiers.CombatTeamStatDefinitionId);
            var borderColor = team.Equals(_combatTeamIdentifiers.PlayerTeamStatValue)
                ? new Color() { R = 0, G = 0, B = 128, A = 0x80 }
                : new Color() { R = 128, G = 0, B = 0, A = 0x80 };
            var backgroundColor = team.Equals(_combatTeamIdentifiers.PlayerTeamStatValue)
                ? new Color() { R = 0, G = 0, B = 128, A = 0x40 }
                : new Color() { R = 128, G = 0, B = 0, A = 0x40 };
            // FIXME: we need a display name behavior
            var actorName = actor.Has<IPlayerControlledBehavior>()
                ? "The Player"
                : "Enemy";
            // FIXME: we need an icon here
            IIdentifier iconResourceId = new StringIdentifier(actorName.Contains("Player")
                ? "graphics/actors/portraits/do-not-distribute/test-player-male"
                : "graphics/actors/portraits/do-not-distribute/test-skeleton");
            var portraitViewModel = new CombatTurnOrderPortraitViewModel(
                actorId,
                borderColor,
                backgroundColor,
                actorName,
                iconResourceId);
            return portraitViewModel;
        }
    }
}
