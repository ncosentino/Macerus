using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Macerus.Plugins.Features.DataPersistence.Kvp;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Logging;
using ProjectXyz.Game.Api;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.CommonBehaviors.Filtering;
using ProjectXyz.Plugins.Features.PartyManagement;
using ProjectXyz.Shared.Framework;

namespace Macerus.Game.DataPersistence.Kvp
{
    public sealed class RosterKvpDataPersistenceHandler :
        IDiscoverableKvpDataPersistenceWriter,
        IDiscoverableKvpDataPersistenceReader
    {
        private static readonly IIdentifier ROSTER_STATE_KEY = new StringIdentifier("RosterActorIds");
        
        private readonly ILogger _logger;
        private readonly Lazy<IRosterManager> _lazyRosterManager;
        private readonly Lazy<IGameObjectRepository> _lazyGameObjectRepository;

        public RosterKvpDataPersistenceHandler(
            ILogger logger,
            Lazy<IRosterManager> lazyRosterManager,
            Lazy<IGameObjectRepository> lazyGameObjectRepository)
        {
            _logger = logger;
            _lazyRosterManager = lazyRosterManager;
            _lazyGameObjectRepository = lazyGameObjectRepository;
        }

        public async Task ReadAsync(IKvpDataStoreReader reader)
        {
            _logger.Debug("Reading roster from data store...");
            _lazyRosterManager.Value.ClearRoster();

            var actorIds = await reader.ReadAsync<IReadOnlyCollection<IIdentifier>>(ROSTER_STATE_KEY);
            _logger.Debug(
                "Actor IDs to load:\r\n" +
                string.Join("\r\n,", actorIds.Select(id => "\t" + id)));

            foreach (var actor in _lazyGameObjectRepository
                .Value
                .Load(new[] { new AnyIdFilter(actorIds) }))
            {
                _logger.Debug($"Adding '{actor}' to roster...");
                _lazyRosterManager.Value.AddToRoster(actor);
            }

            _logger.Debug(
                "Active party:\r\n" +
                string.Join("\r\n,", _lazyRosterManager
                    .Value
                    .ActiveParty
                    .Select(obj => obj.GetOnly<IReadOnlyIdentifierBehavior>().Id)
                    .Select(id => "\t" + id)));

            _logger.Debug("Read roster from data store.");
        }

        public async Task WriteAsync(IKvpDataStoreWriter writer)
        {
            _logger.Debug("Writing roster to data store...");

            var actorIds = _lazyRosterManager
                .Value
                .FullRoster
                .Select(x => x
                    .GetOnly<IReadOnlyIdentifierBehavior>()
                    .Id)
                .ToArray();
            _logger.Debug(
                "Actor IDs to write:\r\n" +
                string.Join("\r\n,", actorIds.Select(id => "\t" + id)));

            await writer.WriteAsync(
                ROSTER_STATE_KEY,
                actorIds);

            _logger.Debug("Wrote roster to data store.");
        }
    }
}
