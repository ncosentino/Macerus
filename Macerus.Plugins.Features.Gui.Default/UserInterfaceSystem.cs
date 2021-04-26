using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Macerus.Plugins.Features.Gui.Api;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.Systems;

namespace Macerus.Plugins.Features.Gui.Default
{
    public sealed class UserInterfaceSystem : IUserInterfaceSystem
    {
        private readonly Dictionary<IUserInterfaceUpdate, DateTime> _lastUpdated;

        private readonly IReadOnlyCollection<Tuple<double, IUserInterfaceUpdate>> _updaters;

        public UserInterfaceSystem(
            IEnumerable<IDiscoverableUserInterfaceUpdate> discoverableUserInterfaceUpdaters)
        {
            _updaters = discoverableUserInterfaceUpdaters
                .Select(x => new Tuple<double, IUserInterfaceUpdate>(
                    x.UpdateIntervalInSeconds,
                    x))
                .ToArray();

            _lastUpdated = _updaters.ToDictionary(x => x.Item2, x => DateTime.UtcNow);
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

            var now = DateTime.UtcNow;

            Parallel.ForEach(
                _updaters.Where(x => (now - _lastUpdated[x.Item2]).TotalSeconds > elapsedSeconds),
                async (x) =>
                {
                    await x.Item2.UpdateAsync(systemUpdateContext);
                    _lastUpdated[x.Item2] = DateTime.UtcNow;
                });
        }
    }
}
