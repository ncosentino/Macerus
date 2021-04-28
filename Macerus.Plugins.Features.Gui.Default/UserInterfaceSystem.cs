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
        private readonly Dictionary<IUserInterfaceUpdate, DateTime> _nextUpdateLookup;

        private readonly IReadOnlyCollection<Tuple<double, IUserInterfaceUpdate>> _updaters;

        public UserInterfaceSystem(
            IEnumerable<IDiscoverableUserInterfaceUpdate> discoverableUserInterfaceUpdaters)
        {
            _updaters = discoverableUserInterfaceUpdaters
                .Select(x => new Tuple<double, IUserInterfaceUpdate>(
                    x.UpdateIntervalInSeconds,
                    x))
                .ToArray();

            _nextUpdateLookup = _updaters.ToDictionary(x => x.Item2, x => DateTime.MinValue);
        }


        public int? Priority => null;

        public void Update(
            ISystemUpdateContext systemUpdateContext,
            IEnumerable<IHasBehaviors> hasBehaviors)
        {
            // we're going to use wall-clock time
            var now = DateTime.UtcNow;

            Parallel.ForEach(
                _updaters.Where(x => now >= _nextUpdateLookup[x.Item2]),
                async (x) =>
                {
                    await x.Item2.UpdateAsync(systemUpdateContext);
                    _nextUpdateLookup[x.Item2] = DateTime.UtcNow.AddSeconds(x.Item1);
                });
        }
    }
}
