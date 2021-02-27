using System.Collections.Generic;
using ProjectXyz.Plugins.Features.StateEnchantments.Api;

namespace Macerus.Plugins.Content.Wip.Enchantments
{
    public sealed class StateIdToTermRepo : IDiscoverableStateIdToTermRepository
    {
        public IEnumerable<IStateIdToTermMapping> GetStateIdToTermMappings()
        {
            yield break;
        }
    }
}
