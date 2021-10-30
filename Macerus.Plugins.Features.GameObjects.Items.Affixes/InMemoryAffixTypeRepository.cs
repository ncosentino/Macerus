using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.GameObjects.Items.Affixes.Api;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.GameObjects.Items.Affixes.Default
{
    public sealed partial class InMemoryAffixTypeRepository : IAffixTypeRepository
    {
        private readonly Dictionary<IIdentifier, IAffixType> _affixTypes;

        public InMemoryAffixTypeRepository(IEnumerable<IAffixType> affixTypes)
        {
            _affixTypes = new Dictionary<IIdentifier, IAffixType>();
            WriteAffixTypes(affixTypes);
        }

        public IEnumerable<IAffixType> GetAllAffixTypes() => _affixTypes.Values;

        public IAffixType GetAffixTypeById(IIdentifier affixId) =>
            _affixTypes.TryGetValue(affixId, out var affixType)
            ? affixType
            : null;

        public IAffixType GetAffixTypeByName(string name) =>
            _affixTypes.FirstOrDefault(x => string.Equals(x.Value.Name, name)).Value;

        public void WriteAffixTypes(IEnumerable<IAffixType> affixTypes)
        {
            foreach (var affixType in affixTypes)
            {
                _affixTypes.Add(affixType.Id, affixType);
            }
        }
    }
}
