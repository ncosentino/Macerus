using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.GameObjects.Skills.Api;

using NexusLabs.Contracts;

using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Api.Enchantments.Generation;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Filtering.Default.Attributes;
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
        private readonly IEnchantmentLoader _enchantmentLoader;

        public SkillAmenity(
            ISkillIdentifiers skillIdentifiers,
            ISkillDefinitionRepositoryFacade skillDefinitionRepositoryFacade,
            ISkillRepository skillRepository,
            IFilterContextFactory filterContextFactory,
            IEnchantmentLoader enchantmentLoader)
        {
            _skillIdentifiers = skillIdentifiers;
            _skillDefinitionRepositoryFacade = skillDefinitionRepositoryFacade;
            _skillRepository = skillRepository;
            _filterContextFactory = filterContextFactory;
            _enchantmentLoader = enchantmentLoader;
        }

        public IGameObject GetSkillById(IIdentifier skillDefinitionId)
        {
            var skills = _skillRepository
                .GetSkills(_filterContextFactory.CreateFilterContextForSingle(new IFilterAttribute[]
                {
                    new FilterAttribute(
                        _skillIdentifiers.SkillDefinitionIdentifier,
                        new IdentifierFilterAttributeValue(skillDefinitionId),
                        true),
                }))
                .ToArray();
            Contract.Requires(
                skills.Length <= 1,
                $"Expecting 0 or 1 skills matching ID '{skillDefinitionId}' but there were {skills.Length}.");
            return skills.FirstOrDefault();
        }

        public IEnumerable<IGameObject> GetStatefulEnchantmentsBySkillId(IIdentifier skillDefinitionId)
        {
            var skillDefinition = _skillDefinitionRepositoryFacade
                .GetSkillDefinitions(_filterContextFactory.CreateFilterContextForSingle(new FilterAttribute(
                    _skillIdentifiers.SkillDefinitionIdentifier,
                    new IdentifierFilterAttributeValue(skillDefinitionId),
                    true)))
                .Single();

            var statefulEnchantments = skillDefinition
                .FilterComponents
                .FirstOrDefault(x => x is PassiveEnchantmentGeneratorComponent) as PassiveEnchantmentGeneratorComponent;

            if (statefulEnchantments == null)
            {
                return Enumerable.Empty<IGameObject>();
            }

            return statefulEnchantments == null
                ? Enumerable.Empty<IGameObject>()
                : _enchantmentLoader.LoadForEnchantmenDefinitionIds(
                    statefulEnchantments.EnchantmentDefinitionIds);
        }
    }
}
