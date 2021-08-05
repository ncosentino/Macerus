using Macerus.Plugins.Features.Combat.Api;
using Macerus.Plugins.Features.Gui.Default;
using Macerus.Plugins.Features.HeaderBar.Api.CombatTurnOrder;
using Macerus.Plugins.Features.Stats;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

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
            var actorName = actor
                .GetOnly<IHasDisplayNameBehavior>()
                .DisplayName;
            var iconResourceId = actor
                .GetOnly<IHasDisplayIconBehavior>()
                .IconResourceId;
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
