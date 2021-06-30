using System.Collections.Generic;
using System.Threading.Tasks;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.Combat.Api
{
    public interface ICombatAI
    {
        Task<bool> ExecuteAsync(
            IGameObject actor,
            IReadOnlyCollection<IGameObject> combatGameObjects,
            IInterval<double> elapsed);
    }
}
