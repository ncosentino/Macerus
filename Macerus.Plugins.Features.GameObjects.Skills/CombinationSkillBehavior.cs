using Macerus.Plugins.Features.GameObjects.Skills.Api;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Shared.Game.Behaviors;

using System.Collections.Generic;
using System.Linq;

namespace Macerus.Plugins.Features.GameObjects.Skills.Default
{

    public sealed class CombinationSkillBehavior : 
        BaseBehavior,
        ICombinationSkillBehavior
    {
        private readonly IGameObjectFactory _gameObjectFactory;

        public CombinationSkillBehavior(
            IGameObjectFactory gameObjectFactory,
            params ISkillExecutorBehavior[] executorBehaviors)
        {
            _gameObjectFactory = gameObjectFactory;
            SkillExecutors = executorBehaviors
                .Select(x => _gameObjectFactory.Create(new[] { x }))
                .ToArray();
        }

        public IReadOnlyCollection<IGameObject> SkillExecutors { get; }
    }
}
