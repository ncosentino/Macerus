using System.Collections.Generic;

using ProjectXyz.Plugins.Features.GameObjects.Enchantments.States;

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
