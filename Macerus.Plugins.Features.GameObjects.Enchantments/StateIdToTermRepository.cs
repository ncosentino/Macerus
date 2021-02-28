using System.Collections.Generic;

using ProjectXyz.Plugins.Features.StateEnchantments.Api;

namespace Macerus.Plugins.Features.GameObjects.Enchantments
{
    public sealed class StateIdToTermRepository : IDiscoverableStateIdToTermRepository
    {
        public IEnumerable<IStateIdToTermMapping> GetStateIdToTermMappings()
        {
            yield break;
        }
    }
}
