using Macerus.Plugins.Features.GameObjects.Skills.Api;

using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.GameObjects.Skills.Default
{
    public sealed class CombinationSkillBehaviorFactory : ICombinationSkillBehaviorFactory
    {
        private readonly IGameObjectFactory _gameObjectFactory;

        public CombinationSkillBehaviorFactory(IGameObjectFactory gameObjectFactory)
        {
            _gameObjectFactory = gameObjectFactory;
        }

        public ICombinationSkillBehavior Create(params ISkillExecutorBehavior[] executorBehaviors)
        {
            var behavior = new CombinationSkillBehavior(
                _gameObjectFactory,
                executorBehaviors);
            return behavior;
        }
    }
}
