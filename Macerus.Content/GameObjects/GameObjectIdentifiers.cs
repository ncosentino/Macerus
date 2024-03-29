﻿using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Shared.Framework;

namespace Macerus.Content.GameObjects
{
    public sealed class GameObjectIdentifiers : IGameObjectIdentifiers
    {
        public IIdentifier FilterContextId { get; } = new StringIdentifier("id");

        public IIdentifier FilterContextTypeId { get; } = new StringIdentifier("type-id");

        public IIdentifier FilterContextTemplateId { get; } = new StringIdentifier("template-id");
    }
}
