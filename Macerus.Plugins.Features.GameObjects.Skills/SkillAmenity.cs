using System.Collections.Generic;
using System.Linq;

using Macerus.Api.Behaviors.Filtering;
using Macerus.Plugins.Features.GameObjects.Skills.Api;

using NexusLabs.Contracts;

using ProjectXyz.Api.Enchantments.Generation;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Skills;
using ProjectXyz.Plugins.Features.GameObjects.Skills.Components;

namespace Macerus.Plugins.Features.GameObjects.Skills.Default
{

    public sealed class SkillAmenity : ISkillAmenity
    {
        private readonly ISkillIdentifiers _skillIdentifiers;
        private readonly ISkillRepository _skillRepository;
        private readonly ISkillDefinitionRepositoryFacade _skillDefinitionRepositoryFacade;
        private readonly IFilterContextFactory _filterContextFactory;
        private readonly IFilterContextAmenity _filterContextAmenity;
        private readonly IEnchantmentLoader _enchantmentLoader;

        public SkillAmenity(
            ISkillIdentifiers skillIdentifiers,
            ISkillDefinitionRepositoryFacade skillDefinitionRepositoryFacade,
            ISkillRepository skillRepository,
            IFilterContextFactory filterContextFactory,
            IEnchantmentLoader enchantmentLoader,
            IFilterContextAmenity filterContextAmenity)
        {
            _skillIdentifiers = skillIdentifiers;
            _skillDefinitionRepositoryFacade = skillDefinitionRepositoryFacade;
            _skillRepository = skillRepository;
            _filterContextFactory = filterContextFactory;
            _enchantmentLoader = enchantmentLoader;
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
