using System;
using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Plugins.Features.GameObjects.Items.Socketing.Api;

namespace Macerus.Plugins.Features.GameObjects.Items.Socketing
{
    public sealed class CanFitSocketFilterComponentToBehaviorConverter : IDiscoverableFilterComponentToBehaviorConverter
    {
        private readonly ICanFitSocketBehaviorFactory _canFitSocketBehaviorFactory;

        public CanFitSocketFilterComponentToBehaviorConverter(ICanFitSocketBehaviorFactory canFitSocketBehaviorFactory)
        {
            _canFitSocketBehaviorFactory = canFitSocketBehaviorFactory;
        }

        public Type ComponentType { get; } = typeof(CanFitSocketFilterComponent);

        public IEnumerable<IBehavior> Convert(IFilterComponent FilterComponent)
        {
            var canFitSocketFilterComponent = (CanFitSocketFilterComponent)FilterComponent;
            var canFitSocketBehavior = _canFitSocketBehaviorFactory.Create(
                canFitSocketFilterComponent.SocketId,
                canFitSocketFilterComponent.Size); ;
            yield return canFitSocketBehavior;
        }
    }
}
