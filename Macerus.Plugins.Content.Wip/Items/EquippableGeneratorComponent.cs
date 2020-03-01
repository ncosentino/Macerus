using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;

namespace Macerus.Plugins.Content.Wip.Items
{
    public sealed class EquippableGeneratorComponent : IGeneratorComponent
    {
        public EquippableGeneratorComponent(IEnumerable<IIdentifier> allowedEquipSlots)
        {
            AllowedEquipSlots = allowedEquipSlots.ToArray();
        }

        public IEnumerable<IGeneratorAttribute> SupportedAttributes { get; } = Enumerable.Empty<IGeneratorAttribute>();

        public IReadOnlyCollection<IIdentifier> AllowedEquipSlots { get; }
    }
}
