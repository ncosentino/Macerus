using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;

namespace Macerus.Shared.Behaviors
{
    public sealed class HasSkillsGeneratorComponent : IGeneratorComponent
    {
        public HasSkillsGeneratorComponent(IEnumerable<KeyValuePair<IIdentifier, int>> skillIdAndLevel)
        {
            SkillIdAndLevel = skillIdAndLevel.ToDictionary(
                x => x.Key,
                x => x.Value);
        }

        public IReadOnlyDictionary<IIdentifier, int> SkillIdAndLevel { get; }
    }
}
