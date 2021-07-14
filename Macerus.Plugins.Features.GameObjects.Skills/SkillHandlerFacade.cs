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

        public async Task HandleAsync(
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
                        await HandleSkillAsync(user, s).ConfigureAwait(false);
                    }

                    continue;
                }

                if (executor is IParallelSkillExecutorBehavior)
                {
                    var tasks = skillsToExecute.Select(s => HandleSkillAsync(user, s));
                    await Task
                        .WhenAll(tasks)
                        .ConfigureAwait(false);
                    continue;
                }
            }
        }

        private async Task HandleSkillAsync(IGameObject user, IGameObject skill)
        {
            foreach (var handler in _skillHandlers)
            {
                await handler.HandleAsync(user, skill);
            }
        }
    }
}
