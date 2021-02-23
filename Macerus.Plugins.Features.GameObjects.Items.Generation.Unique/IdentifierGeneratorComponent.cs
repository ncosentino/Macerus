﻿using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Content.Wip.Items
{
    public sealed class UniqueBaseItemFilterComponent : IFilterComponent
    {
        public UniqueBaseItemFilterComponent(IIdentifier identifier)
        {
            Identifier = identifier;
        }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; } = Enumerable.Empty<IFilterAttribute>();
        
        public IIdentifier Identifier { get; }
    }
}