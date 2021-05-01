using Macerus.Plugins.Features.GameObjects.Skills.Api;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

using System.Collections.Generic;
using System.Linq;

namespace Macerus.Plugins.Features.GameObjects.Skills.Default
{
    public sealed class CombinationSkillBehavior : BaseBehavior, ICombinationSkillBehavior
    {
        public CombinationSkillBehavior(
            params IIdentifier[] skillIds) : this(skillIds.ToList())
        {
        }

        public CombinationSkillBehavior(
            IEnumerable<IIdentifier> skillIds)
        {
            SkillIds = skillIds.ToArray();
        }

        public IReadOnlyCollection<IIdentifier> SkillIds { get; }
    }
}
