using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace Macerus.Api.Behaviors
{
    public interface ITriggerScriptBehavior : IBehavior
    {
        IIdentifier OnEnterTriggerScriptId { get; }

        IIdentifier OnExitTriggerScriptId { get; }
    }
}