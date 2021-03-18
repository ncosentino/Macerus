using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.Encounters
{
    public sealed class EncounterMapFilterBehavior :
        BaseBehavior,
        IEncounterMapFilterBehavior
    {
        public EncounterMapFilterBehavior(IEnumerable<IFilterAttribute> providedAttributes)
        {
            ProvidedAttributes = providedAttributes.ToArray();
        }

        public IReadOnlyCollection<IFilterAttribute> ProvidedAttributes { get; }
    }
}
