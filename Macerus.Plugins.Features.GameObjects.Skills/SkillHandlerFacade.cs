using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Macerus.Plugins.Features.GameObjects.Skills.Api;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Logging;

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
            _skillHandlers = skillHandlers.ToArray();
        }

        public void Handle(
            IGameObject user,
            IGameObject skill)
        {
            // FIXME: Really, this is the same as a combination skill with a single skill inside. 
            // No need for any other types of skills. Can wrap it in some sugarre to make it work 
            // the same as combinatorial ones.
            if (!skill.TryGetFirst<ICombinationSkillBehavior>(out var combinationSkill))
            {
                HandleSkill(user, skill);
                return;
            }

            foreach (var executor in combinationSkill.SkillExecutors)
            {
                if (executor.TryGetFirst<ISequentialSkillExecutorBehavior>(out var sequentialSkills))
                {
                    var skillsToExecuteSequentially = sequentialSkills
                        .SkillIds
                        .Select(x => _skillAmenity.GetSkillById(x))
                        .ToArray();

                    foreach (var s in skillsToExecuteSequentially)
                    {
                        HandleSkill(user, s);
                    }

                    continue;
                }

                if (executor.TryGetFirst<IParallelSkillExecutorBehavior>(out var parallelSkills))
                {
                    var skillsToExecuteInParallel = parallelSkills
                        .SkillIds
                        .Select(x => _skillAmenity.GetSkillById(x))
                        .ToArray();

                    Parallel.ForEach(skillsToExecuteInParallel, (s) => HandleSkill(user, s));

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
