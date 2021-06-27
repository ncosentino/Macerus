using System;
using System.Threading.Tasks;

using Macerus.Plugins.Features.GameObjects.Skills;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.TurnBased;

namespace Macerus.Plugins.Features.GameObjects.Skills.Default
{
    public sealed class ElapseActionSkillHandler : IDiscoverableSkillHandler
    {
        private readonly Lazy<ITurnBasedManager> _lazyTurnBasedManager;

        public ElapseActionSkillHandler(Lazy<ITurnBasedManager> lazyTurnBasedManager)
        {
            _lazyTurnBasedManager = lazyTurnBasedManager;
        }

        public int? Priority { get; } = null;

        public async Task HandleAsync(
            IGameObject user,
            IGameObject skill)
        {
            _lazyTurnBasedManager.Value.NotifyActionTaken(user);
        }
    }
}
