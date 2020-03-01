using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.GameObjects.Enchantments.Generation.Magic
{
    public interface IHasSuffixBehavior : IBehavior
    {
        IIdentifier SuffixId { get; }
    }
}