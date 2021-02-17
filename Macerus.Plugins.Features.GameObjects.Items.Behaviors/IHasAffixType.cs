using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.GameObjects.Items.Behaviors
{
    public interface IHasAffixType : IBehavior
    {
        IIdentifier AffixTypeId { get; }
    }
}