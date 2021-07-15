using System;

using Macerus.Plugins.Features.GameObjects.Skills.Api;

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

        public void Handle(
            IGameObject user,
            IGameObject skill)
        {
            _lazyTurnBasedManager.Value.NotifyActionTaken(user);
        }
    }
}
