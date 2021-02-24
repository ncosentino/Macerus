using System.Collections.Generic;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.ExpressionEnchantments.Api;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Content.Wip.Enchantments
{
    public sealed class ValueMapperRepository : IValueMapperRepository
    {
        private static readonly IInterval SECONDS_PER_TURN = new Interval<double>(1);
        private static readonly IInterval MILLISECONDS_PER_TURN = SECONDS_PER_TURN.Multiply(1000d);

        public IEnumerable<ValueMapperDelegate> GetValueMappers()
        {
            yield return context => new KeyValuePair<string, double>("$PER_TURN", context.Elapsed.Divide(MILLISECONDS_PER_TURN));
        }
    }
}
