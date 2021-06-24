using System.Collections.Generic;
using System.Linq;

using Macerus.Api.Behaviors.Filtering;
using Macerus.Plugins.Features.GameObjects.Skills.Api;

using NexusLabs.Contracts;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Skills;

namespace Macerus.Plugins.Features.GameObjects.Skills.Default
{

    public sealed class SkillAmenity : ISkillAmenity
    {
        private readonly ISkillIdentifiers _skillIdentifiers;
        private readonly ISkillRepository _skillRepository;
        private readonly IFilterContextFactory _filterContextFactory;
        private readonly IFilterContextAmenity _filterContextAmenity;

        public SkillAmenity(
            ISkillIdentifiers skillIdentifiers,
            ISkillRepository skillRepository,
            IFilterContextFactory filterContextFactory,
            IFilterContextAmenity filterContextAmenity)
        {
            _skillIdentifiers = skillIdentifiers;
            _skillRepository = skillRepository;
            _filterContextFactory = filterContextFactory;
            _filterContextAmenity = filterContextAmenity;
        }

        public IGameObject GetSkillById(IIdentifier skillDefinitionId)
        {
            var skills = _skillRepository
                .GetSkills(_filterContextFactory.CreateFilterContextForSingle(new IFilterAttribute[]
                {
                    _filterContextAmenity.CreateRequiredAttribute(
                        _skillIdentifiers.SkillDefinitionIdentifier,
                        skillDefinitionId),
                }))
                .ToArray();
            Contract.Requires(
                skills.Length == 1,
                $"Expecting 1 skill matching ID '{skillDefinitionId}' but there were {skills.Length}.");
            return skills.Single();
        }

        public IEnumerable<IGameObject> GetSkillsFromCombination(IGameObject skill)
        {
            var skillQueue = new Queue<IGameObject>(new[]
            {
                skill
            });

            var visited = new HashSet<IIdentifier>();
            while (skillQueue.Count > 0)
            {
                var currentSkill = skillQueue.Dequeue();
                var skillId = currentSkill.GetOnly<IReadOnlyIdentifierBehavior>().Id;
                if (visited.Contains(skillId))
                {
                    continue;
                }

                visited.Add(skillId);

                var leafNode = true;
                if (currentSkill.TryGetFirst<ICombinationSkillBehavior>(out var combinationBehavior))
                {
                    foreach (var skillExecutor in combinationBehavior.SkillExecutors)
                    {
                        foreach (var subSkillId in skillExecutor.SkillIdentifiers)
                        {
                            var subSkill = GetSkillById(subSkillId);
                            skillQueue.Enqueue(subSkill);
                            leafNode = false;
                        }
                    }
                }

                if (leafNode)
                {
                    yield return currentSkill;
                }
            }
        }

        public bool TryGetSkillById(
            IIdentifier skillDefinitionId,
            out IGameObject skill)
        {
            var skills = _skillRepository
                .GetSkills(_filterContextFactory.CreateFilterContextForSingle(new IFilterAttribute[]
                {
                    _filterContextAmenity.CreateRequiredAttribute(
                        _skillIdentifiers.SkillDefinitionIdentifier,
                        skillDefinitionId),
                }))
                .ToArray();
            skill = skills.Length == 1
                ? skills.Single()
                : null;
            return skills.Length == 1;
        }
    }
}
