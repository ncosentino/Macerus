using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.Mapping
{
    public interface IMappingAmenity : IReadOnlyMappingAmenity
    {
        void MarkForAddition(IGameObject obj);
    }
}