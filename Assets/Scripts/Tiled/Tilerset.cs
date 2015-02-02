using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Tiled
{
    public class Tileset
    {
        #region Fields
        private readonly string _name;
        private readonly int _tileWidth;
        private readonly int _tileHeight;
        private readonly List<TilesetImage> _images;
        private readonly List<TilesetTile> _tiles;
        #endregion

        #region Constructors
        public Tileset(string name, int tileWidth, int tileHeight, IEnumerable<TilesetImage> images, IEnumerable<TilesetTile> tiles)
        {
            _name = name;
            _tileWidth = tileWidth;
            _tileHeight = tileHeight;
            _images = new List<TilesetImage>(images);
            _tiles = new List<TilesetTile>(tiles);
        }
        #endregion
    }
}
