using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.Mapping;

using NexusLabs.Framework;

using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Systems;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.TurnBased;

namespace Macerus.Plugins.Features.GameObjects.Actors.Default.AI
{
    public sealed class WalkZoneAISystem : IDiscoverableSystem
    {
        private readonly IRandom _random;
        private readonly Lazy<IMappingAmenity> _lazyMappingAmenity;

        public WalkZoneAISystem(
            IRandom random,
            Lazy<IMappingAmenity> lazyMappingAmenity)
        {
            _random = random;
            _lazyMappingAmenity = lazyMappingAmenity;
        }

        public int? Priority => null;

        public async Task UpdateAsync(ISystemUpdateContext systemUpdateContext)
        {
            var turnInfo = systemUpdateContext
                .GetFirst<IComponent<ITurnInfo>>()
                .Value;

            var tasks = GetGameObjectsWithAITaskToInitialize(turnInfo.ApplicableGameObjects)
                .Select(objectWithAITask =>
                {
                    var gameObject = objectWithAITask.Item1;
                    var aiBehavior = objectWithAITask.Item2;
                    var walkZoneAITaskBehavior = objectWithAITask.Item3;
                    return InitializeWalkZoneAsync(gameObject, aiBehavior, walkZoneAITaskBehavior);
                })
                .Concat(GetGameObjectsWithAITask(turnInfo.ApplicableGameObjects)
                .Select(objectWithAITask =>
                {
                    var gameObject = objectWithAITask.Item1;
                    var aiBehavior = objectWithAITask.Item2;
                    var walkZoneAITaskBehavior = objectWithAITask.Item3;
                    return PerformWalkZoneAsync(gameObject, aiBehavior, walkZoneAITaskBehavior);
                }));
            await Task
                .WhenAll(tasks)
                .ConfigureAwait(false);
        }

        private async Task InitializeWalkZoneAsync(
            IGameObject gameObject,
            IAIBehavior aiBehavior,
            IWalkZoneAITaskBehavior walkZoneAITaskBehavior)
        {
            var movementBehavior = gameObject.GetOnly<IMovementBehavior>();
            var positionBehavior = gameObject.GetOnly<IReadOnlyPositionBehavior>();
            var sizeBehavior = gameObject.GetOnly<IReadOnlySizeBehavior>();

            var currentPosition = new Vector2((float)positionBehavior.X, (float)positionBehavior.Y);
            var targetPosition = new Vector2(
                (float)(walkZoneAITaskBehavior.X + walkZoneAITaskBehavior.Width * _random.NextDouble(0, 1)),
                (float)(walkZoneAITaskBehavior.Y + walkZoneAITaskBehavior.Height * _random.NextDouble(0, 1)));

            //var walkPath = _lazyMappingAmenity.Value.CurrentPathFinder.FindPath(
            //    currentPosition,
            //    targetPosition,
            //    new Vector2((float)sizeBehavior.Width, (float)sizeBehavior.Height),
            //    true);
            //movementBehavior.SetWalkPath(walkPath.Positions);
            movementBehavior.SetWalkPath(new[]
            {
                currentPosition,
                targetPosition,
            });

            aiBehavior.Tasks.Enqueue(walkZoneAITaskBehavior);
        }

        private async Task PerformWalkZoneAsync(
            IGameObject gameObject,
            IAIBehavior aiBehavior,
            IWalkZoneAITaskBehavior walkZoneAITaskBehavior)
        {
            var doneWalking = !gameObject.GetOnly<IReadOnlyMovementBehavior>().PointsToWalk.Any();
            if (!doneWalking)
            {
                return;
            }

            if (!aiBehavior.Tasks.TryDequeue(out var dequeued) ||
                dequeued != walkZoneAITaskBehavior)
            {
                throw new InvalidOperationException(
                    $"Expecting to dequeue '{walkZoneAITaskBehavior}' from the " +
                    $"AI tasks for '{gameObject}' but got '{dequeued}'.");
            }
        }

        private IEnumerable<Tuple<IGameObject, IAIBehavior, IWalkZoneAITaskBehavior>> GetGameObjectsWithAITask(IEnumerable<IGameObject> gameObjects)
        {
            foreach (var gameObject in gameObjects)
            {
                if (!gameObject.TryGetFirst<IAIBehavior>(out var aiBehavior))
                {
                    continue;
                }

                var currentTask = aiBehavior.Tasks.FirstOrDefault();
                if (currentTask == null ||
                    !(currentTask is IWalkZoneAITaskBehavior walkZoneAITaskBehavior))
                {
                    continue;
                }

                var result = Tuple.Create(
                    gameObject,
                    aiBehavior,
                    walkZoneAITaskBehavior);
                yield return result;
            }
        }

        private IEnumerable<Tuple<IGameObject, IAIBehavior, IWalkZoneAITaskBehavior>> GetGameObjectsWithAITaskToInitialize(IEnumerable<IGameObject> gameObjects)
        {
            foreach (var gameObject in gameObjects)
            {
                if (!gameObject.TryGetFirst<IAIBehavior>(out var aiBehavior))
                {
                    continue;
                }

                var currentTask = aiBehavior.TasksToInitialize.FirstOrDefault();
                if (currentTask == null ||
                    !(currentTask is IWalkZoneAITaskBehavior walkZoneAITaskBehavior))
                {
                    continue;
                }

                if (!aiBehavior.TasksToInitialize.TryDequeue(out var dequeued) ||
                    dequeued != walkZoneAITaskBehavior)
                {
                    throw new InvalidOperationException(
                        $"Expecting to dequeue '{walkZoneAITaskBehavior}' from the " +
                        $"AI tasks to initialize for '{gameObject}' but got '{dequeued}'.");
                }

                var result = Tuple.Create(
                    gameObject,
                    aiBehavior,
                    walkZoneAITaskBehavior);
                yield return result;
            }
        }
    }
}
