using Macerus.Api.Behaviors;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Shared.Behaviors
{
    public sealed class TriggerScriptBehavior :
        BaseBehavior,
        ITriggerScriptBehavior
    {
        public TriggerScriptBehavior(
            IIdentifier onEnterTriggerScriptId,
            IIdentifier onExitTriggerScriptId)
        {
            OnEnterTriggerScriptId = onEnterTriggerScriptId;
            OnExitTriggerScriptId = onExitTriggerScriptId;
        }

        public IIdentifier OnEnterTriggerScriptId { get; }

        public IIdentifier OnExitTriggerScriptId { get; }
    }
}