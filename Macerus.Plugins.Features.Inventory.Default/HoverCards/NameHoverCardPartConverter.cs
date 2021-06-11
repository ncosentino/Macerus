using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.GameObjects.Items.Behaviors;
using Macerus.Plugins.Features.Inventory.Api.HoverCards;

using ProjectXyz.Api.GameObjects.Behaviors;

namespace Macerus.Plugins.Features.Inventory.Default.HoverCards
{
    public sealed class NameHoverCardPartConverter : IDiscoverableBehaviorsToHoverCardPartViewModelConverter
    {
        public IEnumerable<IHoverCardPartViewModel> Convert(IEnumerable<IBehavior> behaviors)
        {
            var nameBehaviors = behaviors
                .Get<IHasInventoryDisplayName>()
                .ToArray();
            if (!nameBehaviors.Any())
            {
                yield break;
            }
            
            if (nameBehaviors.Length == 1)
            {
                yield return new SingleNameHoverCardPartViewModel(nameBehaviors.Single().DisplayName);
                yield break;
            }

            if (nameBehaviors.Length == 2)
            {
                yield return new MultiNameHoverCardPartViewModel(
                    nameBehaviors[1].DisplayName,
                    $"({nameBehaviors[0].DisplayName})");
                yield break;
            }

            throw new NotSupportedException("FIXME: something else could *technically* support this, but we should likely handle it here if it's valid");
        }
    }
}
