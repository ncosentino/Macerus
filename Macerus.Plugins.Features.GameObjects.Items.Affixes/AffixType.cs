using Macerus.Plugins.Features.GameObjects.Items.Affixes.Api;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.GameObjects.Items.Affixes
{
    public sealed class AffixType : IAffixType
    {
        public IIdentifier Id { get; }

        public AffixType(
            IIdentifier id,
            string name)
        {
            Id = id;
            Name = name;
        }

        public string Name { get; }
    }
}
