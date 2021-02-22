using System;
using System.Collections.Generic;
using System.Linq;

using NexusLabs.Framework;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Plugins.Features.GameObjects.Items.Socketing.Api;

namespace Macerus.Plugins.Features.GameObjects.Items.Socketing
{
    public sealed class SocketFilterComponentToBehaviorConverter : IDiscoverableFilterComponentToBehaviorConverter
    {
        private readonly IApplySocketEnchantmentsBehaviorFactory _applySocketEnchantmentsBehaviorFactory;
        private readonly ICanBeSocketedBehaviorFactory _canBeSocketedBehaviorFactory;
        private readonly IRandom _random;

        public SocketFilterComponentToBehaviorConverter(
            ICanBeSocketedBehaviorFactory canBeSocketedBehaviorFactory,
            IRandom random,
            IApplySocketEnchantmentsBehaviorFactory applySocketEnchantmentsBehaviorFactory)
        {
            _canBeSocketedBehaviorFactory = canBeSocketedBehaviorFactory;
            _random = random;
            _applySocketEnchantmentsBehaviorFactory = applySocketEnchantmentsBehaviorFactory;
        }

        public Type ComponentType { get; } = typeof(SocketFilterComponent);

        public IEnumerable<IBehavior> Convert(IFilterComponent FilterComponent)
        {
            var socketFilterComponent = (SocketFilterComponent)FilterComponent;

            // FIXME: we'll want to look at randomizing the order of these...
            var generatedSockets = socketFilterComponent
                .SocketRanges
                .SelectMany(kvp => Enumerable
                .Repeat(
                    kvp.Key,
                    _random.Next(
                        kvp.Value.Item1,
                        kvp.Value.Item2)))
                .Take(socketFilterComponent.MaximumSockets)
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
