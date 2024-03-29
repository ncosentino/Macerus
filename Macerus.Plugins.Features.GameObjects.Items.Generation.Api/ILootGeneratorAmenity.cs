﻿using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Api
{
    public interface ILootGeneratorAmenity
    {
        IEnumerable<IGameObject> GenerateLoot(
            IIdentifier dropTableId,
            IFilterContext lootGeneratorContext);
    }
}
