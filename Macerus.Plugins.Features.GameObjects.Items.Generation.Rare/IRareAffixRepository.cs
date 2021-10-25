using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Rare
{
    public interface IRareAffixRepository
    {
        IEnumerable<IRareItemAffix> GetAffixes(IFilterContext filterContext);
    }
}