using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Magic.Enchantments
{
    public interface IHasPrefixBehavior : IBehavior
    {
        IIdentifier PrefixStringResourceId { get; }
    }
}