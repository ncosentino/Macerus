using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Magic.Enchantments
{
    public interface IHasSuffixBehavior : IBehavior
    {
        IIdentifier SuffixId { get; }
    }
}