
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Magic
{
    public sealed class MagicItemAffixGeneratorComponent : IGeneratorComponent
    {
        public MagicItemAffixGeneratorComponent(
            IIdentifier prefixStringResourceId, 
            IIdentifier suffixStringResourceId)
        {
            PrefixStringResourceId = prefixStringResourceId;
            SuffixStringResourceId = suffixStringResourceId;
        }

        public IIdentifier PrefixStringResourceId { get; }

        public IIdentifier SuffixStringResourceId { get; }
    }
}
