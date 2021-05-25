using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace Macerus.Plugins.Features.GameObjects.Actors.Generation
{
    public interface IDiscoverableActorGenerator : 
        IActorGenerator,
        IHasFilterAttributes
    {
    }
}