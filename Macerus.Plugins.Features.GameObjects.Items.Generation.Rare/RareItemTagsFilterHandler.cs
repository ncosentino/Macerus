using System.Linq;

using ProjectXyz.Plugins.Features.CommonBehaviors.Filtering;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Rare
{
    public sealed class RareItemTagsFilterHandler
    {
        public RareItemTagsFilterHandler()
        {
            Matcher = (filter1, filter2) =>
            {
                var match = filter1.Tags.Any(t1 => filter2.Tags.Any(t2 => t1.Equals(t2)));
                return match;
            };
        }

        public GenericAttributeValueMatchDelegate<AnyTagsFilter, AnyTagsFilter> Matcher;
    }
}