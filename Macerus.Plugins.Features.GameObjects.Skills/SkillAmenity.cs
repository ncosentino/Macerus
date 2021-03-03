using System.Linq;

using NexusLabs.Contracts;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Skills;

namespace Macerus.Plugins.Features.GameObjects.Skills
{
    public sealed class SkillAmenity : ISkillAmenity
    {
        private readonly ISkillIdentifiers _skillIdentifiers;
        private readonly ISkillRepository _skillRepository;
        private readonly IFilterContextFactory _filterContextFactory;

        public SkillAmenity(
            ISkillIdentifiers skillIdentifiers,
            ISkillRepository skillRepository,
            IFilterContextFactory filterContextFactory)
        {
            _skillIdentifiers = skillIdentifiers;
            _skillRepository = skillRepository;
            _filterContextFactory = filterContextFactory;
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
    }
}
