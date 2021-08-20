using System.Collections.Generic;
using System.Numerics;

namespace Macerus.Plugins.Features.Mapping
{
    public sealed class NoneMapTraversableHighlighter : IMapTraversableHighlighter
    {
        public void SetTargettedTiles(IReadOnlyDictionary<int, HashSet<Vector2>> targettedTiles)
        {
        }

        public void SetTraversableTiles(IEnumerable<Vector2> traversableTiles)
        {
        }
    }
}