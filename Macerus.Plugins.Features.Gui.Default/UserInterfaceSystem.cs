using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Macerus.Plugins.Features.Gui.Api;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Systems;

namespace Macerus.Plugins.Features.Gui.Default
{
    public sealed class UserInterfaceSystem : IUserInterfaceSystem
    {
        private readonly Dictionary<IDiscoverableUserInterfaceUpdate, DateTime> _lastUpdateLookup;

        private readonly IReadOnlyCollection<IDiscoverableUserInterfaceUpdate> _updaters;

        public UserInterfaceSystem(
            IEnumerable<IDiscoverableUserInterfaceUpdate> discoverableUserInterfaceUpdaters)
        {
            _updaters = discoverableUserInterfaceUpdaters.ToArray();

            _lastUpdateLookup = _updaters.ToDictionary(x => x, x => DateTime.MinValue);
        }

        public int? Priority => null;

        public async Task UpdateAsync(
            ISystemUpdateContext systemUpdateContext,
            IEnumerable<IGameObject> gameObjects)
        {
            // we're going to use wall-clock time
            var now = DateTime.UtcNow;

            var updaters = _updaters
                .Where(x => now >= _lastUpdateLookup[x].AddSeconds(x.UpdateIntervalInSeconds))
                .ToArray();

            foreach (var updater in updaters)
            {
                await updater.UpdateAsync(systemUpdateContext);
                _lastUpdateLookup[updater] = DateTime.UtcNow;
            }
        }
    }
}
