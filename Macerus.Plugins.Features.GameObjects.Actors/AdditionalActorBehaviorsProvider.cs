﻿using System;
using System.Collections.Generic;
using Macerus.Shared.Behaviors;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;

namespace Macerus.Plugins.Features.GameObjects.Actors
{
    public sealed class AdditionalActorBehaviorsProvider : IAdditionalActorBehaviorsProvider
    {
        public IEnumerable<IBehavior> GetBehaviors()
        {
            yield return new WorldLocationBehavior();
        }
    }
}
