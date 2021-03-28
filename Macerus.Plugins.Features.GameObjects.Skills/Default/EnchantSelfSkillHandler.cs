using System.Collections.Generic;

using Macerus.Plugins.Features.GameObjects.Skills.Api;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Macerus.Plugins.Features.GameObjects.Skills.Default
{
    public sealed class EnchantSelfSkillHandler : IDiscoverableSkillHandler
    {
        private readonly ISkillAmenity _skillAmenity;

        public EnchantSelfSkillHandler(ISkillAmenity skillAmenity)
        {
            _skillAmenity = skillAmenity;
        }

        public void Handle(
            IGameObject user,
            IGameObject skill,
            IReadOnlyCollection<IGameObject> targets)
        {
            var skillDefinitionId = skill
                .GetOnly<ITemplateIdentifierBehavior>()
                .TemplateId;
            var statefulEnchantments = _skillAmenity.GetStatefulEnchantmentsBySkillId(skillDefinitionId);
            var targetEnchantmentsBehavior = user.GetOnly<IHasEnchantmentsBehavior>();
            targetEnchantmentsBehavior.AddEnchantments(statefulEnchantments);
        }
    }
}
