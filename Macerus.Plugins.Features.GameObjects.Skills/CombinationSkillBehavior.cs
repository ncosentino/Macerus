using Macerus.Plugins.Features.GameObjects.Skills.Api;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macerus.Plugins.Features.GameObjects.Skills.Default
{
    public sealed class CombinationSkillBehavior : BaseBehavior, ICombinationSkillBehavior
    {
        public CombinationSkillBehavior(
            IEnumerable<IIdentifier> skillIds)
        {
            SkillIds = skillIds.ToArray();
        }

        public IReadOnlyCollection<IIdentifier> SkillIds { get; }
    }
}
