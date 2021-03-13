using ProjectXyz.Api.Behaviors.Filtering.Attributes;

namespace Macerus.Api.GameObjects
{
    public interface IDiscoverableGameObjectRepository :
        IGameObjectRepository,
        IHasFilterAttributes
    {
    }
}