using System;

using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.Encounters.Triggers
{
    public sealed class GameObjectTriggerEventArgs : EventArgs
    {
        public GameObjectTriggerEventArgs(IGameObject collidingGameObject)
        {
            CollidingGameObject = collidingGameObject;
        }

        public IGameObject CollidingGameObject { get; }
    }
}
