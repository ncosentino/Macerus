using System.Collections.Generic;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace Macerus.Plugins.Features.Encounters
{
    public interface IEncounterMapFilterBehavior : IBehavior
    {
        IReadOnlyCollection<IFilterAttribute> ProvidedAttributes { get; }
    }
}
