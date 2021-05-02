using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Macerus.Plugins.Features.GameObjects.Skills.Api;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Logging;
using ProjectXyz.Plugins.Features.GameObjects.Skills;

namespace Macerus.Plugins.Features.GameObjects.Skills.Default
{
    public sealed class SkillHandlerFacade : ISkillHandlerFacade
    {
        private readonly IReadOnlyCollection<IDiscoverableSkillHandler> _skillHandlers;
        private readonly ILogger _logger;
        private readonly ISkillAmenity _skillAmenity;

        public SkillHandlerFacade(
            ILogger logger,
            ISkillAmenity skillAmenity,
            IEnumerable<IDiscoverableSkillHandler> skillHandlers)
        {
            _logger = logger;
            _skillAmenity = skillAmenity;
            _skillHandlers = skillHandlers
                .OrderBy(x => x.Priority == null
                    ? int.MaxValue
                    : x.Priority.Value)
                .ToArray();
        }

        public void Handle(
            IGameObject user,
            IGameObject skill)
        {
            // All skills should be a combination skill (even a combination of one!)
            if (!skill.TryGetFirst<ICombinationSkillBehavior>(out var combinationSkill))
            {
                return;
            }

            foreach (var executor in combinationSkill.SkillExecutors)
            {
                var skillsToExecute = executor
                    .SkillIdentifiers
                    .Select(x => _skillAmenity.GetSkillById(x))
                    .ToArray();

                if (executor is ISequentialSkillExecutorBehavior)
                {
                    foreach (var s in skillsToExecute)
                    {
                        HandleSkill(user, s);
                    }

                    continue;
                }

                if (executor is IParallelSkillExecutorBehavior)
                {
                    Parallel.ForEach(skillsToExecute, (s) => HandleSkill(user, s));

                    continue;
                }
            }
        }

        private void HandleSkill(IGameObject user, IGameObject skill)
         {
             foreach (var handler in _skillHandlers)
             {
                 handler.Handle(user, skill);
             }
         }
    }
}
