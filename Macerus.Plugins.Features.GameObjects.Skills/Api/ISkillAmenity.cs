using System.Collections.Generic;

using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.GameObjects.Skills.Api
{
    public interface ISkillAmenity
    {
        IGameObject GetSkillById(IIdentifier skillDefinitionId);

        IEnumerable<IEnchantment> GetStatefulEnchantmentsBySkillId(IIdentifier skillDefinitionId);
    }
}