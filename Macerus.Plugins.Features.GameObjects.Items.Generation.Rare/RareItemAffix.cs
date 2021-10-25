using System.Collections.Generic;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Rare
{
    public sealed class RareItemAffix : IRareItemAffix
    {
        public RareItemAffix(
            IIdentifier stringResourceId, 
            IEnumerable<IFilterAttribute> supportedAttributes)
        {
            StringResourceId = stringResourceId;
            SupportedAttributes = supportedAttributes;
        }

        public IIdentifier StringResourceId { get; }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; }
    }
}
