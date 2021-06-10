﻿using System.Collections.Generic;
using System.Numerics;

namespace Macerus.Plugins.Features.Mapping
{
    public interface IMapTraversableHighlighter
    {
        void SetTraversableTiles(IEnumerable<Vector2> traversableTiles);
    }
}