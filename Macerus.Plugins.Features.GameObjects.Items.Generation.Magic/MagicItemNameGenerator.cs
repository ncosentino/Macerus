using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.GameObjects.Items.Behaviors;
using Macerus.Plugins.Features.GameObjects.Items.Generation.Magic.Enchantments;
using Macerus.Plugins.Features.Resources;

using NexusLabs.Framework;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Magic
{
    public sealed class MagicItemNameGenerator : IMagicItemNameGenerator
    {
        private readonly IRandom _random;
        private readonly IStringResourceProvider _stringResourceProvider;

        public MagicItemNameGenerator(
            IRandom random,
            IStringResourceProvider stringResourceProvider)
        {
            _random = random;
            _stringResourceProvider = stringResourceProvider;
        }

        public IHasInventoryDisplayName GenerateName(
            IEnumerable<IBehavior> itemBehaviors,
            IReadOnlyCollection<IGameObject> enchantments)
        {
            // FIXME: make this a bit more robust. it assumes every enchantment
            // MUST exist as a prefix AND a suffix, and we can pick and choose
            // at will. what if we want to change that assumption? what if some
            // enchantments can only exist as a prefix *OR* a suffix?
            var prefixes = enchantments
                .ToDictionary(
                    x => x,
                    x => x.GetOnly<IHasPrefixBehavior>());
            var suffixes = enchantments
                .ToDictionary(
                    x => x,
                    x => x.GetOnly<IHasSuffixBehavior>());
            var baseDisplayName = itemBehaviors.GetOnly<IHasInventoryDisplayName>();
            if (enchantments.Count == 1)
            {
                return new HasInventoryDisplayName(_random.NextDouble(0, 1) >= 0.5
                    ? $"{baseDisplayName.DisplayName} {_stringResourceProvider.GetString(suffixes.Single().Value.SuffixStringResourceId)}"
                    : $"{_stringResourceProvider.GetString(prefixes.Single().Value.PrefixStringResourceId)} {baseDisplayName.DisplayName}");
            }

            var prefixEnchantmentIndex = _random.Next(0, 2);
            var suffixEnchantmentIndex = prefixEnchantmentIndex == 0
                ? 1
                : 0;
            var prefix = prefixes[prefixEnchantmentIndex == 0 ? enchantments.First() : enchantments.Last()];
            var suffix = suffixes[suffixEnchantmentIndex == 0 ? enchantments.First() : enchantments.Last()];

            return new HasInventoryDisplayName(
                $"{_stringResourceProvider.GetString(prefix.PrefixStringResourceId)} " +
                $"{baseDisplayName.DisplayName} " +
                $"{_stringResourceProvider.GetString(suffix.SuffixStringResourceId)}");
        }
    }
}
