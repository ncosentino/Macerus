using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Maps.Tiled
{
    public interface ITiledMap
    {
        #region Properties
        int Width { get; }
        
        int Height { get; }
        
        int TileWidth { get; }
        
        int TileHeight { get; }
        
        IEnumerable<Tileset> Tilesets { get; }
        
        IEnumerable<MapLayer> Layers { get; }
        
        IEnumerable<ObjectLayer> ObjectLayers { get; }
        #endregion
    }
}
