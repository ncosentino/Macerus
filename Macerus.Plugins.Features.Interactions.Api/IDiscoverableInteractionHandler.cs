using System;

namespace Macerus.Plugins.Features.Interactions.Api
{
    public interface IDiscoverableInteractionHandler : IInteractionHandler
    {
        Type InteractableType { get; }
    }
}