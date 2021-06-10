using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.GameObjects.Items.Behaviors;

using NexusLabs.Framework;

using ProjectXyz.Api.Framework.Collections;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Rare
{
    public sealed class RareItemNameGenerator : IRareItemNameGenerator
    {
        private readonly IRandom _random;
        private readonly IRareAffixRepository _rareAffixRepository;

        public RareItemNameGenerator(
            IRandom random,
            IRareAffixRepository rareAffixRepository)
        {
            _random = random;
            _rareAffixRepository = rareAffixRepository;
        }

        public IHasInventoryDisplayName GenerateName(
            IEnumerable<IBehavior> itemBehaviors,
            IReadOnlyCollection<IGameObject> enchantments)
        {
            // FIXME: later we need to be able to filter the affixes by the types of items
            // i.e. "Entropy Chain" is a good amulet name, not a good sword name
            var prefix = _rareAffixRepository.GetAffixes(true).Random(_random);
            var suffix = _rareAffixRepository.GetAffixes(false).Random(_random);

            return new HasInventoryDisplayName($"{prefix} {suffix}");
        }
    }
}
