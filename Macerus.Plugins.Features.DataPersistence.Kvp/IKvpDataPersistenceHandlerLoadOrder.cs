﻿namespace Macerus.Plugins.Features.DataPersistence.Kvp
{
    public interface IKvpDataPersistenceHandlerLoadOrder
    {
        int GetOrder(IKvpDataPersistenceWriter writer);

        int GetOrder(IKvpDataPersistenceReader reader);
    }
}
