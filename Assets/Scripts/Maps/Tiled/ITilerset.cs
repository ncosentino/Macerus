using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Maps.Tiled
{
    public interface ITileset
    {
        #region Properties
        int FirstGid { get; }

        string Name { get; }

        int TileWidth { get; }

        int TileHeight { get; }

        IEnumerable<TilesetImage> Images { get; }
        
        IEnumerable<TilesetTile> Tiles { get; }
        #endregion
    }
}
