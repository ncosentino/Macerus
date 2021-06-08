using System.Numerics;

namespace Macerus.Plugins.Features.Mapping
{
    public interface IMapHoverSelectFormatter
    {
        void HoverSelectTile(Vector2? position);
    }
}