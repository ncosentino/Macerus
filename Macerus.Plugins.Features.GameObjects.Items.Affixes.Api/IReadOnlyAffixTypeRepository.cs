using System.Collections.Generic;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.GameObjects.Items.Affixes.Api
{
    public interface IReadOnlyAffixTypeRepository
    {
        IAffixType GetAffixTypeById(IIdentifier affixId);

        IAffixType GetAffixTypeByName(string name);

        IEnumerable<IAffixType> GetAllAffixTypes();
    }
}