using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.Combat.Api
{
    public interface ICombatAI
    {
        void Execute(
            IGameObject actor,
            IReadOnlyCollection<IGameObject> combatGameObjects);
    }
}
