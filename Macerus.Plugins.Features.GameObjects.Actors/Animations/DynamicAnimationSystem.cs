using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Macerus.Api.Behaviors;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Systems;

namespace Macerus.Plugins.Features.GameObjects.Actors.Animations
{
    public sealed class DynamicAnimationSystem : IDiscoverableSystem
    {
        public int? Priority => null;

        public void Update(
            ISystemUpdateContext systemUpdateContext,
            IEnumerable<IHasBehaviors> hasBehaviors)
        {
            var elapsed = (IInterval<double>)systemUpdateContext
                .GetFirst<IComponent<IElapsedTime>>()
                .Value
                .Interval;
            var elapsedSeconds = elapsed.Value / 1000;

            Parallel.ForEach(
                GetDynamicAnimationBehaviors(hasBehaviors),
                dynamicAnimationBehavior =>
                {
                    dynamicAnimationBehavior.UpdateAnimation(elapsedSeconds);
                });
        }

        private IEnumerable<IDynamicAnimationBehavior> GetDynamicAnimationBehaviors(IEnumerable<IHasBehaviors> hasBehaviors)
        {
            return hasBehaviors.SelectMany(x => x.Get<IDynamicAnimationBehavior>());
        }
    }
}
