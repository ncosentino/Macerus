using ProjectXyz.Api.Behaviors.Filtering.Attributes;

namespace Macerus.Plugins.Features.GameObjects.Actors.Generation
{
    public interface IDiscoverableActorGenerator : 
        IActorGenerator,
        IHasFilterAttributes
    {
    }
}