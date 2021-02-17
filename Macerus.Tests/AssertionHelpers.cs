using System.Linq;

using Macerus.Plugins.Features.GameObjects.Items.Behaviors;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.GameObjects.Items.Socketing.Api;

using Xunit;

namespace Macerus.Tests
{
    public static class AssertionHelpers
    {
        public static void AssertSocketBehaviors(IHasBehaviors item)
        {
            var canBeSocketedBehaviors = item
                .Get<ICanBeSocketedBehavior>()
                .ToArray();
            Assert.InRange(canBeSocketedBehaviors.Length, 0, 1);
            if (canBeSocketedBehaviors.Length > 0)
            {
                Assert.InRange(
                    canBeSocketedBehaviors.Single().TotalSockets.Sum(x => x.Value),
                    1,
                    8); // current max limit we're interested in
                Assert.Equal(
                    canBeSocketedBehaviors.Single().TotalSockets.Sum(x => x.Value),
                    canBeSocketedBehaviors.Single().AvailableSockets.Sum(x => x.Value));
                Assert.Equal(0, canBeSocketedBehaviors.Single().OccupiedSockets.Count);
                Assert.Single(item.Get<IApplySocketEnchantmentsBehavior>());
            }
        }

        public static void AssertAffix(IHasBehaviors item, IIdentifier requiredAffixId)
        {
            var affixTypeBehaviors = item
                .Get<IHasAffixType>()
                .ToArray();
            Assert.True(
                affixTypeBehaviors.Length == 1,
                $"We expect all items to have exactly 1 affix type. This " +
                $"item had {affixTypeBehaviors.Length} " +
                $"{typeof(IHasAffixType)} behaviors.");
            Assert.Equal(affixTypeBehaviors.Single().AffixTypeId, requiredAffixId);
        }
    }
}
