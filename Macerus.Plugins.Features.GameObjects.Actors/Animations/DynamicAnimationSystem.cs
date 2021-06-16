using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Macerus.Api.Behaviors;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Systems;

namespace Macerus.Plugins.Features.GameObjects.Actors.Animations
{
    public sealed class DynamicAnimationSystem : IDiscoverableSystem
    {
        public int? Priority => null;

        public async Task UpdateAsync(
            ISystemUpdateContext systemUpdateContext,
            IEnumerable<IGameObject> gameObjects)
        {
            var elapsed = (IInterval<double>)systemUpdateContext
                .GetFirst<IComponent<IElapsedTime>>()
                .Value
                .Interval;
            var elapsedSeconds = elapsed.Value / 1000;
            await Task
               .WhenAll(GetDynamicAnimationBehaviors(gameObjects).Select(b => b.UpdateAnimationAsync(elapsedSeconds)))
               .ConfigureAwait(false);
        }

        private IEnumerable<IDynamicAnimationBehavior> GetDynamicAnimationBehaviors(IEnumerable<IGameObject> gameObjects)
        {
            return gameObjects.SelectMany(x => x.Get<IDynamicAnimationBehavior>());
        }
    }
}
