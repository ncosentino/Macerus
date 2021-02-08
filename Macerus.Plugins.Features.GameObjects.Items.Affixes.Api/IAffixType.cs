using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.GameObjects.Items.Affixes.Api
{
    public interface IAffixType
    {
        IIdentifier Id { get; }

        string Name { get; }
    }
}