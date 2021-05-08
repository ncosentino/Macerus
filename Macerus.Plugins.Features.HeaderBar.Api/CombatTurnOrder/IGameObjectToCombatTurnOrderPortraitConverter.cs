using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.HeaderBar.Api.CombatTurnOrder
{
    public interface IGameObjectToCombatTurnOrderPortraitConverter
    {
        ICombatTurnOrderPortraitViewModel Convert(IGameObject actor);
    }
}
