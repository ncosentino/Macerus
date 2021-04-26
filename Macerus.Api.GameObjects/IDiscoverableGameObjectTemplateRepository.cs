using ProjectXyz.Api.Behaviors.Filtering.Attributes;

namespace Macerus.Api.GameObjects
{
    public interface IDiscoverableGameObjectTemplateRepository :
        IGameObjectTemplateRepository,
        IHasFilterAttributes
    {
    }
}