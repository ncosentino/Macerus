using System.Collections.Generic;

using ProjectXyz.Api.GameObjects.Behaviors;

namespace Macerus.Plugins.Features.Inventory.Api.HoverCards
{
    public interface IBehaviorsToHoverCardPartViewModelConverter
    {
        IEnumerable<IHoverCardPartViewModel> Convert(IEnumerable<IBehavior> behaviors);
    }
}
