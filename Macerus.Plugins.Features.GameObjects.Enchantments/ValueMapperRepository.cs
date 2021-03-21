using System.Collections.Generic;

using ProjectXyz.Plugins.Features.ExpressionEnchantments.Api;

namespace Macerus.Plugins.Features.GameObjects.Enchantments
{
    public sealed class ValueMapperRepository : IValueMapperRepository
    {
        public IEnumerable<ValueMapperDelegate> GetValueMappers()
        {
            yield return context => new KeyValuePair<string, double>("$PER_TURN", context.ElapsedTurns);
        }
    }
}
