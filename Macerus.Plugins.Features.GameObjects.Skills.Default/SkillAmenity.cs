using System.Collections.Generic;
using System.Linq;

using Macerus.Api.Behaviors.Filtering;

using NexusLabs.Contracts;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Skills;
using ProjectXyz.Plugins.Features.GameObjects.Skills.Effects;

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

        public IGameObject EnsureHasSkill(
            IGameObject actor,
            IIdentifier skillDefinitionId)
        {
            var skillsBehavior = actor.GetOnly<IHasSkillsBehavior>();

            var skill = skillsBehavior
                .Skills
                .FirstOrDefault(x => Equals(
                    x.GetOnly<IReadOnlyIdentifierBehavior>().Id,
                    skillDefinitionId));
            if (skill != null)
            {
                return skill;
            }

            skill = GetSkillById(skillDefinitionId);
            skillsBehavior.Add(new[] { skill });
            return skill;
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

        public bool IsPurelyPassiveSkill(IGameObject skill)
        {
            var passive = GetAllSkillEffects(skill).All(x => x.Has<IPassiveSkillEffectBehavior>());
            return passive;
        }

        public IEnumerable<IGameObject> GetAllSkillEffects(IGameObject skill)
        {
            var skillEffectBehavior = skill.GetOnly<ISkillEffectBehavior>();

            foreach (var executor in skillEffectBehavior.EffectExecutors)
            {
                foreach (var executorBehavior in executor.Get<ISkillEffectExecutorBehavior>())
                {
                    foreach (var skillEffect in executorBehavior.Effects)
                    {
                        yield return skillEffect;
                    }
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
