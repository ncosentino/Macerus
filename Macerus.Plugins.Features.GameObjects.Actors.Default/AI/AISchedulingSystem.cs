using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using NexusLabs.Contracts;
using NexusLabs.Framework;

using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Systems;
using ProjectXyz.Plugins.Features.TurnBased;

namespace Macerus.Plugins.Features.GameObjects.Actors.Default.AI
{
    public sealed class AISchedulingSystem : IDiscoverableSystem
    {
        private readonly IRandom _random;

        public AISchedulingSystem(IRandom random)
        {
            _random = random;
        }

        public int? Priority => null;

        public async Task UpdateAsync(ISystemUpdateContext systemUpdateContext)
        {
            var turnInfo = systemUpdateContext
                .GetFirst<IComponent<ITurnInfo>>()
                .Value;

            var tasks = GetGameObjectsWithUnscheduledAI(turnInfo.ApplicableGameObjects)
                .Select(objectWithUnscheduledAI =>
                {
                    var gameObject = objectWithUnscheduledAI.Item1;
                    var aiBehavior = objectWithUnscheduledAI.Item2;
                    var possibleTasks = objectWithUnscheduledAI.Item3;
                    return ScheduleTaskAsync(gameObject, aiBehavior, possibleTasks);
                });
            await Task
                .WhenAll(tasks)
                .ConfigureAwait(false);
        }

        private async Task ScheduleTaskAsync(
            IGameObject gameObject,
            IAIBehavior aiBehavior,
            IEnumerable<IAITaskBehavior> possibleTasks)
        {
            Contract.Requires(
                possibleTasks.Any(),
                $"'{gameObject}' did not have any possible '{typeof(IAITaskBehavior)}' instances to schedule.");

            var nextTask = GetNextAITask(possibleTasks);
            if (nextTask == null)
            {
                throw new InvalidOperationException(
                    $"No '{typeof(IAITaskBehavior)}' was scheduled for '{gameObject}'.");
            }

            aiBehavior.TasksToInitialize.Enqueue(nextTask);
        }

        private IEnumerable<Tuple<IGameObject, IAIBehavior, IEnumerable<IAITaskBehavior>>> GetGameObjectsWithUnscheduledAI(IEnumerable<IGameObject> gameObjects)
        {
            foreach (var gameObject in gameObjects)
            {
                if (!gameObject.TryGetFirst<IAIBehavior>(out var aiBehavior) ||
                    aiBehavior.Tasks.Any())
                {
                    continue;
                }

                var result = Tuple.Create(
                    gameObject,
                    aiBehavior,
                    gameObject.Behaviors.TakeTypes<IAITaskBehavior>());
                yield return result;
            }
        }

        private IAITaskBehavior GetNextAITask(IEnumerable<IAITaskBehavior> tasks)
        {
            var totalWeight = tasks.Sum(x => x.Weight);
            var randomNumber = _random.NextDouble(0, totalWeight);

            foreach (var task in tasks)
            {
                if (randomNumber < task.Weight)
                {
                    return task;
                }

                randomNumber = randomNumber - task.Weight;
            }

            return null;
        }
    }
}
