using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using Macerus.Plugins.Features.GameObjects.Items.Behaviors;
using Macerus.Plugins.Features.Resources;

using NexusLabs.Contracts;
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

        public IHasInventoryDisplayName GenerateName(IEnumerable<IBehavior> itemBehaviors)
        {
            var affixes = itemBehaviors.Get<IHasMagicAffixBehavior>().ToArray();
            Contract.Requires(
                affixes.Length > 0,
                $"There were no '{typeof(IHasMagicAffixBehavior)}' instances.");
            Contract.Requires(
                affixes.Length <= 2,
                $"There were {affixes.Length} '{typeof(IHasMagicAffixBehavior)}' instances when maximum of 2 were expected.");

            // FIXME: this code has the assumption that every single affix has BOTH a prefix and a suffix.
            var baseItemDisplayName = itemBehaviors.GetOnly<IBaseItemInventoryDisplayName>();
            if (affixes.Length == 1)
            {
                var affix = affixes.Single();
                if (_random.NextDouble(0, 1) >= 0.5)
                {
                    return new HasMagicInventoryDisplayName(
                        null,
                        affix.SuffixStringResourceId,
                        baseItemDisplayName.BaseItemStringResourceId,
                        $"{baseItemDisplayName.DisplayName} {_stringResourceProvider.GetString(affix.SuffixStringResourceId, CultureInfo.InvariantCulture)}");
                }
                
                return new HasMagicInventoryDisplayName(
                    affix.PrefixStringResourceId,
                    null,
                    baseItemDisplayName.BaseItemStringResourceId,
                    $"{_stringResourceProvider.GetString(affixes.Single().PrefixStringResourceId, CultureInfo.InvariantCulture)} {baseItemDisplayName.DisplayName}");
            }

            var prefixAffixIndex = _random.Next(0, 2);
            var suffixAffixIndex = prefixAffixIndex == 0
                ? 1
                : 0;
            var prefix = prefixAffixIndex == 0 ? affixes.First() : affixes.Last();
            var suffix = suffixAffixIndex == 0 ? affixes.First() : affixes.Last();

            // FIXME: we either set the culture here to be explicitly english,
            // or we're generating items fixed at the language of the
            // system... truly, this behavior should NOT do the stringification
            // but instead leave things as resources to work with and be
            // interpreted by a UI later on...
            return new HasMagicInventoryDisplayName(
                prefix?.PrefixStringResourceId,
                suffix?.SuffixStringResourceId,
                baseItemDisplayName.BaseItemStringResourceId,
                $"{_stringResourceProvider.GetString(prefix.PrefixStringResourceId, CultureInfo.InvariantCulture)} " +
                $"{baseItemDisplayName.DisplayName} " +
                $"{_stringResourceProvider.GetString(suffix.SuffixStringResourceId, CultureInfo.InvariantCulture)}");
        }
    }
}
