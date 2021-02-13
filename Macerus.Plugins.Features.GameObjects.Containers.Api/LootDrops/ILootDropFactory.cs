﻿using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.GameObjects.Containers.Api.LootDrops
{
    public interface ILootDropFactory
    {
        IGameObject CreateLoot(
            double worldX,
            double worldY,
            IEnumerable<IGameObject> items);

        IGameObject CreateLoot(
            double worldX,
            double worldY,
            IGameObject item,
            params IGameObject[] items);
    }
}