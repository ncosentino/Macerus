using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Macerus.Plugins.Features.DataPersistence.Default
{
    public sealed class DataPersistenceHandlerFacade : IDataPersistenceHandlerFacade
    {
        private readonly IReadOnlyCollection<IDiscoverableDataPersistenceHandler> _handlers;

        public DataPersistenceHandlerFacade(IEnumerable<IDiscoverableDataPersistenceHandler> handlers)
        {
            _handlers = handlers.ToArray();
        }

        public async Task WriteAsync(IDataStore dataStore)
        {
            var handler = _handlers.FirstOrDefault(x => x.CanWrite(dataStore));
            if (handler == null)
            {
                throw new NotSupportedException(
                    $"There is no handler registered that can support data store '{dataStore}'.");
            }

            await handler.WriteAsync(dataStore);
        }
    }
}
