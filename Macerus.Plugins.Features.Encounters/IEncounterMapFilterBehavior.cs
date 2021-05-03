﻿using System.Collections.Generic;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;

namespace Macerus.Plugins.Features.Encounters
{
    public interface IEncounterMapFilterBehavior : IBehavior
    {
        IReadOnlyCollection<IFilterAttribute> ProvidedAttributes { get; }
    }
}
