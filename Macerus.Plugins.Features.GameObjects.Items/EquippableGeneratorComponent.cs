using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;

namespace Macerus.Plugins.Features.GameObjects.Items
{
    public sealed class EquippableGeneratorComponent : IGeneratorComponent
    {
        public EquippableGeneratorComponent(IEnumerable<IIdentifier> allowedEquipSlots)
        {
            AllowedEquipSlots = allowedEquipSlots.ToArray();
        }

        public IReadOnlyCollection<IIdentifier> AllowedEquipSlots { get; }
    }
}
