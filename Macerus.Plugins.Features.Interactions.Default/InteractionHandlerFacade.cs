using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Macerus.Plugins.Features.Interactions.Api;

using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.Interactions.Default
{
    public sealed class InteractionHandlerFacade : IInteractionHandlerFacade
    {
        private readonly Dictionary<Type, IDiscoverableInteractionHandler> _handlers;

        public InteractionHandlerFacade(IEnumerable<IDiscoverableInteractionHandler> handlers)
        {
            _handlers = handlers.ToDictionary(
                x => x.InteractableType,
                x => x);
        }

        public async Task InteractAsync(
            IGameObject actor,
            IInteractableBehavior behavior)
        {
            if (!_handlers.TryGetValue(
                behavior.GetType(),
                out var handler))
            {
                throw new InvalidOperationException(
                    $"There were no '{typeof(IInteractionHandler)}' instances " +
                    $"to handle '{behavior}'.");
            }

            await handler
                .InteractAsync(actor, behavior)
                .ConfigureAwait(false);
        }
    }
}
