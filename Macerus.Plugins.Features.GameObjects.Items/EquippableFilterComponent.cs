using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.GameObjects.Items
{
    public sealed class EquippableFilterComponent : IFilterComponent
    {
        public EquippableFilterComponent(IEnumerable<IIdentifier> allowedEquipSlots)
        {
            AllowedEquipSlots = allowedEquipSlots.ToArray();
        }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; } = Enumerable.Empty<IFilterAttribute>();

        public IReadOnlyCollection<IIdentifier> AllowedEquipSlots { get; }
    }
}
