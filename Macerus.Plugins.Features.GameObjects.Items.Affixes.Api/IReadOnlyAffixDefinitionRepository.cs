using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Filtering.Api;

namespace Macerus.Plugins.Features.GameObjects.Items.Affixes.Api
{
    public interface IReadOnlyAffixDefinitionRepository
    {
        IEnumerable<IAffixDefinition> GetAffixDefinitions(IFilterContext filterContext);
    }
}