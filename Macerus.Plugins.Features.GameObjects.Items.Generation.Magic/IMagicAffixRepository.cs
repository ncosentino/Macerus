using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Magic
{
    public interface IMagicAffixRepository
    {
        string GetAffix(IIdentifier affixId);
    }
}