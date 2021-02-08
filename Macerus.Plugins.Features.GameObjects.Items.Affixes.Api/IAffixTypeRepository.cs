using System.Collections.Generic;

namespace Macerus.Plugins.Features.GameObjects.Items.Affixes.Api
{
    public interface IAffixTypeRepository : IReadOnlyAffixTypeRepository
    {
        void WriteAffixTypes(IEnumerable<IAffixType> affixTypes);
    }
}