using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Macerus.Plugins.Features.DataPersistence.Kvp;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
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

        private readonly Lazy<IRosterManager> _lazyRosterManager;
        private readonly Lazy<IGameObjectRepository> _lazyGameObjectRepository;

        public RosterKvpDataPersistenceHandler(
            Lazy<IRosterManager> lazyRosterManager,
            Lazy<IGameObjectRepository> lazyGameObjectRepository)
        {
            _lazyRosterManager = lazyRosterManager;
            _lazyGameObjectRepository = lazyGameObjectRepository;
        }

        public async Task ReadAsync(IKvpDataStoreReader reader)
        {
            _lazyRosterManager.Value.ClearRoster();

            var actorIds = (IReadOnlyCollection<IIdentifier>)await reader.ReadAsync(ROSTER_STATE_KEY);
            foreach (var actor in _lazyGameObjectRepository
                .Value
                .Load(new[] { new AnyIdFilter(actorIds) }))
            {                
                _lazyRosterManager.Value.AddToRoster(actor);
            }
        }

        public async Task WriteAsync(IKvpDataStoreWriter writer)
        {
            await writer.WriteAsync(
                ROSTER_STATE_KEY,
                _lazyRosterManager
                    .Value
                    .FullRoster
                    .Select(x => x
                        .GetOnly<IReadOnlyIdentifierBehavior>()
                        .Id)
                    .ToArray());
        }
    }
}
