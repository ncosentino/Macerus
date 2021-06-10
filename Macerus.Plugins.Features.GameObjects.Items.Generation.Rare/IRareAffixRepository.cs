using System.Collections.Generic;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Rare
{
    public interface IRareAffixRepository
    {
        IEnumerable<string> GetAffixes(bool prefix);
    }
}