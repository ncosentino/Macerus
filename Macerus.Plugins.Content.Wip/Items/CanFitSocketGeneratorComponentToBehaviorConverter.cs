using System;
using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Items.Socketing.Api;

namespace Macerus.Plugins.Content.Wip.Items
{
    public sealed class CanFitSocketGeneratorComponentToBehaviorConverter : IDiscoverableGeneratorComponentToBehaviorConverter
    {
        private readonly ICanFitSocketBehaviorFactory _canFitSocketBehaviorFactory;

        public CanFitSocketGeneratorComponentToBehaviorConverter(ICanFitSocketBehaviorFactory canFitSocketBehaviorFactory)
        {
            _canFitSocketBehaviorFactory = canFitSocketBehaviorFactory;
        }

        public Type ComponentType { get; } = typeof(CanFitSocketGeneratorComponent);

        public IEnumerable<IBehavior> Convert(IGeneratorComponent generatorComponent)
        {
            var canFitSocketGeneratorComponent = (CanFitSocketGeneratorComponent)generatorComponent;
            var canFitSocketBehavior = _canFitSocketBehaviorFactory.Create(
                canFitSocketGeneratorComponent.SocketId,
                canFitSocketGeneratorComponent.Size); ;
            yield return canFitSocketBehavior;
        }
    }
}
