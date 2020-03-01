using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.GameObjects.Enchantments.Generation.Magic
{
    public interface IHasPrefixBehavior : IBehavior
    {
        IIdentifier PrefixId { get; }
    }
}