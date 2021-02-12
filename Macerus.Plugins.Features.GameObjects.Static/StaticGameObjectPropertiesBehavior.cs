using System.Collections.Generic;

using Macerus.Plugins.Features.GameObjects.Static.Api;

using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Static
{
    public sealed class StaticGameObjectPropertiesBehavior :
        BaseBehavior,
        IReadOnlyStaticGameObjectPropertiesBehavior
    {
        public StaticGameObjectPropertiesBehavior(IReadOnlyDictionary<string, object> properties)
        {
            Properties = properties;
        }

        public IReadOnlyDictionary<string, object> Properties { get; }
    }
}
