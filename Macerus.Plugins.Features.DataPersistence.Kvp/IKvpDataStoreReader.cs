﻿using System.Collections.Generic;
using System.Threading.Tasks;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.DataPersistence.Kvp
{
    public interface IKvpDataStoreReader
    {
        IEnumerable<IIdentifier> GetKeys();

        Task<T> ReadAsync<T>(IIdentifier identifier);
    }
}
