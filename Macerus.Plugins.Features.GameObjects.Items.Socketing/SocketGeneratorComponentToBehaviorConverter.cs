using System;
using System.Collections.Generic;
using System.Linq;

using NexusLabs.Framework;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.Socketing.Api;

namespace Macerus.Plugins.Features.GameObjects.Items.Socketing
{
    public sealed class SocketGeneratorComponentToBehaviorConverter : IDiscoverableGeneratorComponentToBehaviorConverter
    {
        private readonly IApplySocketEnchantmentsBehaviorFactory _applySocketEnchantmentsBehaviorFactory;
        private readonly ICanBeSocketedBehaviorFactory _canBeSocketedBehaviorFactory;
        private readonly IRandom _random;

        public SocketGeneratorComponentToBehaviorConverter(
            ICanBeSocketedBehaviorFactory canBeSocketedBehaviorFactory,
            IRandom random,
            IApplySocketEnchantmentsBehaviorFactory applySocketEnchantmentsBehaviorFactory)
        {
            _canBeSocketedBehaviorFactory = canBeSocketedBehaviorFactory;
            _random = random;
            _applySocketEnchantmentsBehaviorFactory = applySocketEnchantmentsBehaviorFactory;
        }

        public Type ComponentType { get; } = typeof(SocketGeneratorComponent);

        public IEnumerable<IBehavior> Convert(
            IFilterContext filterContext,
            IEnumerable<IBehavior> baseBehaviors,
            IGeneratorComponent generatorComponent)
        {
            var socketGeneratorComponent = (SocketGeneratorComponent)generatorComponent;

            // FIXME: we'll want to look at randomizing the order of these...
            var generatedSockets = socketGeneratorComponent
                .SocketRanges
                .SelectMany(kvp => Enumerable
                .Repeat(
                    kvp.Key,
                    _random.Next(
                        kvp.Value.Item1,
                        kvp.Value.Item2)))
                .Take(socketGeneratorComponent.MaximumSockets)
                .ToArray();
            if (generatedSockets.Length < 1)
            {
                yield break;
            }

            var canBeSocketedBehavior = _canBeSocketedBehaviorFactory.Create(generatedSockets);
            yield return canBeSocketedBehavior;
            yield return _applySocketEnchantmentsBehaviorFactory.Create();
        }
    }
}
