using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using NexusLabs.Framework;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Systems;
using ProjectXyz.Plugins.Features.TurnBased;

namespace Macerus.Plugins.Features.GameObjects.Actors.Default.AI
{
    public sealed class IdleAISystem : IDiscoverableSystem
    {
        private readonly IRandom _random;

        public IdleAISystem(IRandom random)
        {
            _random = random;
        }

        public int? Priority => null;

        public async Task UpdateAsync(ISystemUpdateContext systemUpdateContext)
        {
            var turnInfo = systemUpdateContext
                .GetFirst<IComponent<ITurnInfo>>()
                .Value;
            var elapsedTime = systemUpdateContext
                .GetFirst<IComponent<IElapsedTime>>()
                .Value;
            var elapsedSeconds = ((IInterval<double>)elapsedTime.Interval).Value / 1000;

            var tasks = GetGameObjectsWithAITaskToInitialize(turnInfo.ApplicableGameObjects)
                .Select(objectWithAITask =>
                {
                    var gameObject = objectWithAITask.Item1;
                    var aiBehavior = objectWithAITask.Item2;
                    var idleAITaskBehavior = objectWithAITask.Item3;
                    return InitializeIdleWaitAsync(gameObject, aiBehavior, idleAITaskBehavior);
                })
                .Concat(GetGameObjectsWithAITask(turnInfo.ApplicableGameObjects)
                .Select(objectWithAITask =>
                {
                    var gameObject = objectWithAITask.Item1;
                    var aiBehavior = objectWithAITask.Item2;
                    var idleAITaskBehavior = objectWithAITask.Item3;
                    return PerformIdleWaitAsync(
                        gameObject, 
                        aiBehavior,
                        idleAITaskBehavior,
                        elapsedSeconds);
                }));
            await Task
                .WhenAll(tasks)
                .ConfigureAwait(false);
        }

        private async Task InitializeIdleWaitAsync(
            IGameObject gameObject,
            IAIBehavior aiBehavior,
            IIdleAITaskBehavior idleAITaskBehavior)
        {
            var randomTimeRange = idleAITaskBehavior.MaximumTargetIdleTime - idleAITaskBehavior.MinimumTargetIdleTime;
            idleAITaskBehavior.RemainingIdleTime =
                idleAITaskBehavior.MinimumTargetIdleTime +
                TimeSpan.FromSeconds(
                    _random.NextDouble(0, randomTimeRange.TotalSeconds));
            aiBehavior.Tasks.Enqueue(idleAITaskBehavior);
        }

        private async Task PerformIdleWaitAsync(
            IGameObject gameObject,
            IAIBehavior aiBehavior,
            IIdleAITaskBehavior idleAITaskBehavior,
            double elapsedSeconds)
        {
            idleAITaskBehavior.RemainingIdleTime = idleAITaskBehavior.RemainingIdleTime - TimeSpan.FromSeconds(elapsedSeconds);
            if (idleAITaskBehavior.RemainingIdleTime > TimeSpan.Zero)
            {
                return;
            }

            if (!aiBehavior.Tasks.TryDequeue(out var dequeued) ||
                dequeued != idleAITaskBehavior)
            {
                throw new InvalidOperationException(
                    $"Expecting to dequeue '{idleAITaskBehavior}' from the " +
                    $"AI tasks for '{gameObject}' but got '{dequeued}'.");
            }
        }

        private IEnumerable<Tuple<IGameObject, IAIBehavior, IIdleAITaskBehavior>> GetGameObjectsWithAITask(IEnumerable<IGameObject> gameObjects)
        {
            foreach (var gameObject in gameObjects)
            {
                if (!gameObject.TryGetFirst<IAIBehavior>(out var aiBehavior))
                {
                    continue;
                }

                var currentTask = aiBehavior.Tasks.FirstOrDefault();
                if (currentTask == null ||
                    !(currentTask is IIdleAITaskBehavior idleAITaskBehavior))
                {
                    continue;
                }

                var result = Tuple.Create(
                    gameObject,
                    aiBehavior,
                    idleAITaskBehavior);
                yield return result;
            }
        }

        private IEnumerable<Tuple<IGameObject, IAIBehavior, IIdleAITaskBehavior>> GetGameObjectsWithAITaskToInitialize(IEnumerable<IGameObject> gameObjects)
        {
            foreach (var gameObject in gameObjects)
            {
                if (!gameObject.TryGetFirst<IAIBehavior>(out var aiBehavior))
                {
                    continue;
                }

                var currentTask = aiBehavior.TasksToInitialize.FirstOrDefault();
                if (currentTask == null ||
                    !(currentTask is IIdleAITaskBehavior idleAITaskBehavior))
                {
                    continue;
                }

                if (!aiBehavior.TasksToInitialize.TryDequeue(out var dequeued) ||
                    dequeued != idleAITaskBehavior)
                {
                    throw new InvalidOperationException(
                        $"Expecting to dequeue '{idleAITaskBehavior}' from the " +
                        $"AI tasks to initialize for '{gameObject}' but got '{dequeued}'.");
                }

                var result = Tuple.Create(
                    gameObject,
                    aiBehavior,
                    idleAITaskBehavior);
                yield return result;
            }
        }
    }
}
