using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ProjectXyz.Api.Systems;
using Macerus.Plugins.Features.Gui.Api;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Entities;
using System;

namespace Macerus.Plugins.Features.Gui.Default
{
    public sealed class UserInterfaceSystem : IUserInterfaceSystem
    {
        private readonly List<Tuple<double, Func<Task>>> _callbacks;

        public UserInterfaceSystem()
        {
            _callbacks = new List<Tuple<double, Func<Task>>>();
        }


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
                _callbacks,
                async (x) =>
                {
                    await Task.Run(x.Item2);
                });
        }

        public void RegisterUpdater(double interval, Func<Task> updateTaskFactory)
        {
            _callbacks.Add(Tuple.Create(interval, updateTaskFactory));
        }
    }
}
