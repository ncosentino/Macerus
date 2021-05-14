using Macerus.Plugins.Features.Interactions.Api;

using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Actors
{
    public sealed class CorpseInteractableBehavior :
        BaseBehavior,
        IInteractableBehavior
    {
        public CorpseInteractableBehavior(
            bool automaticInteraction)
        {
            AutomaticInteraction = automaticInteraction;
        }

        public bool AutomaticInteraction { get; }
    }
}
