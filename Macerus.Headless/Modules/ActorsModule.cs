using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autofac;

using Macerus.Api.Behaviors;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Systems;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.TurnBased;

namespace Macerus.Headless.Modules
{
    public sealed class ActorsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<FakeMovementSystem>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }

        private sealed class FakeMovementSystem : IDiscoverableSystem
        {
            private readonly IBehaviorFinder _behaviorFinder;

            public FakeMovementSystem(IBehaviorFinder behaviorFinder)
            {
                _behaviorFinder = behaviorFinder;
            }

            public int? Priority => null;

            public async Task UpdateAsync(ISystemUpdateContext systemUpdateContext)
            {
                var elapsedTime = systemUpdateContext
                    .GetFirst<IComponent<IElapsedTime>>()
                    .Value;
                var elapsedSeconds = ((IInterval<double>)elapsedTime.Interval).Value / 1000;
                var gameObjects = systemUpdateContext
                    .GetFirst<IComponent<ITurnInfo>>()
                    .Value
                    .AllGameObjects;

                foreach (var gameObject in gameObjects)
                {
                    await Task.Yield();

                    if (!_behaviorFinder.TryFind<IReadOnlyMovementBehavior, IPositionBehavior>(
                        gameObject,
                        out var behaviors))
                    {
                        continue;
                    }

                    var movementBehavior = behaviors.Item1;
                    var locationBehavior = behaviors.Item2;

                    locationBehavior.SetPosition(
                        locationBehavior.X + movementBehavior.VelocityX * elapsedSeconds,
                        locationBehavior.Y + movementBehavior.VelocityY * elapsedSeconds);
                }
            }
        }
    }
}
