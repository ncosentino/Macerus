using System.Collections.Generic;
using ProjectXyz.Plugins.Features.ExpressionEnchantments.Api;

namespace Macerus.Plugins.Content.Wip.Enchantments
{
    public sealed class ValueMapperRepository : IValueMapperRepository
    {
        public IEnumerable<ValueMapperDelegate> GetValueMappers()
        {
            yield break;
        }
    }
}
