using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Tiled
{
    public class TiledMap : ITiledMap
    {
        #region Fields
        private readonly int _width;
        private readonly int _height;
        private readonly int _tileWidth;
        private readonly int _tileHeight;
        private readonly List<Tileset> _tilesets;
        private readonly List<MapLayer> _layers;
        private readonly List<ObjectLayer> _objectLayers;
        #endregion

        #region Constructors
        public TiledMap(
            int width, 
            int height, 
            int tileWidth, 
            int tileHeight, 
            IEnumerable<Tileset> tilesets, 
            IEnumerable<MapLayer> layers, 
            IEnumerable<ObjectLayer> objectLayers)
        {
            _width = width;
            _height = height;
            _tileWidth = tileWidth;
            _tileHeight = tileHeight;
            _tilesets = new List<Tileset>(tilesets);
            _layers = new List<MapLayer>(layers);
            _objectLayers = new List<ObjectLayer>(objectLayers);
        }
        #endregion

        #region Properties
        public int Width
        {
            get { return _width; }
        }

        public int Height
        {
            get { return _height; }
        }

        public int TileWidth
        {
            get { return _tileWidth; }
        }

        public int TileHeight
        {
            get { return _tileHeight; }
        }

        public IEnumerable<Tileset> Tilesets
        {
            get { return _tilesets; }
        }

        public IEnumerable<MapLayer> Layers
        {
            get { return _layers; }
        }

        public IEnumerable<ObjectLayer> ObjectLayers
        {
            get { return _objectLayers; }
        }
        #endregion
    }
}
