using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.GameObjects.Enchantments.Generation.Magic;
using Macerus.Plugins.Features.GameObjects.Items.Behaviors;

using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Magic
{
    public sealed class MagicItemNameGenerator : IMagicItemNameGenerator
    {
        private readonly IRandomNumberGenerator _randomNumberGenerator;
        private readonly IMagicAffixRepository _magicAffixRepository;

        public MagicItemNameGenerator(
            IRandomNumberGenerator randomNumberGenerator,
            IMagicAffixRepository magicAffixRepository)
        {
            _randomNumberGenerator = randomNumberGenerator;
            _magicAffixRepository = magicAffixRepository;
        }

        public IHasInventoryDisplayName GenerateName(
            IGameObject baseItem,
            IReadOnlyCollection<IEnchantment> enchantments)
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
            var baseDisplayName = baseItem.GetOnly<IHasInventoryDisplayName>();
            if (enchantments.Count == 1)
            {
                return new HasInventoryDisplayName(_randomNumberGenerator.NextDouble() >= 0.5
                    ? $"{baseDisplayName.DisplayName} {_magicAffixRepository.GetAffix(suffixes.Single().Value.SuffixId)}"
                    : $"{_magicAffixRepository.GetAffix(prefixes.Single().Value.PrefixId)} {baseDisplayName.DisplayName}");
            }

            var prefixEnchantmentIndex = _randomNumberGenerator.NextInRange(0, 1);
            var suffixEnchantmentIndex = prefixEnchantmentIndex == 0
                ? 1
                : 0;
            var prefix = prefixes[prefixEnchantmentIndex == 0 ? enchantments.First() : enchantments.Last()];
            var suffix = suffixes[suffixEnchantmentIndex == 0 ? enchantments.First() : enchantments.Last()];

            return new HasInventoryDisplayName(
                $"{_magicAffixRepository.GetAffix(prefix.PrefixId)} " +
                $"{baseDisplayName.DisplayName} " +
                $"{_magicAffixRepository.GetAffix(suffix.SuffixId)}");
        }
    }
}
