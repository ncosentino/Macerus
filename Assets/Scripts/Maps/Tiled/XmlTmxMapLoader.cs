using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Assets.Scripts.Maps.Tiled
{
    public class XmlTmxMapLoader
    {
        #region Methods
        public TiledMap ReadXml(string xmlTmxContents)
        {
            using (var reader = XmlReader.Create(new StringReader(xmlTmxContents)))
            {
                return ReadXml(reader);
            }
        }

        public TiledMap ReadXml(XmlReader reader)
        {
            while (reader.Read())
            {
                if (reader.NodeType != XmlNodeType.Element ||
                    reader.Name != "map")
                {
                    continue;
                }

                var width = int.Parse(reader.GetAttribute("width"), CultureInfo.InvariantCulture);
                var height = int.Parse(reader.GetAttribute("height"), CultureInfo.InvariantCulture);
                var tileWidth = int.Parse(reader.GetAttribute("tilewidth"), CultureInfo.InvariantCulture);
                var tileHeight = int.Parse(reader.GetAttribute("tileheight"), CultureInfo.InvariantCulture);

                List<Tileset> tilesets;
                List<MapLayer> layers;
                List<ObjectLayer> objectLayers;
                ReadMapContents(
                    reader.ReadSubtree(),
                    out tilesets,
                    out layers,
                    out objectLayers);

                return new TiledMap(
                    width,
                    height,
                    tileWidth,
                    tileHeight,
                    tilesets,
                    layers,
                    objectLayers);
            }

            throw new FormatException("Could not find the map element.");
        }

        private void ReadMapContents(XmlReader reader, out List<Tileset> tilesets, out List<MapLayer> layers, out List<ObjectLayer> objectLayers)
        {
            tilesets = new List<Tileset>();
            layers = new List<MapLayer>();
            objectLayers = new List<ObjectLayer>();

            while (reader.Read())
            {
                if (reader.NodeType != XmlNodeType.Element)
                {
                    continue;
                }

                switch (reader.Name)
                {
                    case "tileset":
                        tilesets.Add(ReadTileset(reader));
                        break;
                    case "layer":
                        layers.Add(ReadLayer(reader));
                        break;
                    case "objectgroup":
                        objectLayers.Add(ReadObjectLayer(reader));
                        break;
                } 
            }
        }

        private ObjectLayer ReadObjectLayer(XmlReader reader)
        {
            var layerName = reader.GetAttribute("name");
            
            var objects = ReadMapObjects(reader.ReadSubtree());

            return new ObjectLayer(
                layerName, 
                objects);
        }

        private IEnumerable<TiledMapObject> ReadMapObjects(XmlReader reader)
        {
            while (reader.Read())
            {
                if (reader.NodeType != XmlNodeType.Element ||
                    reader.Name != "object")
                {
                    continue;
                }

                var id = reader.GetAttribute("id");
                var objectName = reader.GetAttribute("name");
                var type = reader.GetAttribute("type");
                var gid = reader.GetAttribute("gid");
                var x = float.Parse(reader.GetAttribute("x"), CultureInfo.InvariantCulture);
                var y = float.Parse(reader.GetAttribute("y"), CultureInfo.InvariantCulture);

                var widthAttr = reader.GetAttribute("width");
                var width = widthAttr == null
                    ? (float?)null
                    : float.Parse(widthAttr, CultureInfo.InvariantCulture);

                var heightAttr = reader.GetAttribute("height");
                var height = heightAttr == null
                    ? (float?)null
                    : float.Parse(heightAttr, CultureInfo.InvariantCulture);

                var properties = ReadTilesetTileProperties(reader.ReadSubtree());

                yield return new TiledMapObject(
                    id,
                    objectName,
                    type,
                    gid,
                    x,
                    y,
                    width,
                    height,
                    properties);
            }
        }

        private MapLayer ReadLayer(XmlReader reader)
        {
            var layerName = reader.GetAttribute("name");
            var width = int.Parse(reader.GetAttribute("width"), CultureInfo.InvariantCulture);
            var height = int.Parse(reader.GetAttribute("height"), CultureInfo.InvariantCulture);

            var tiles = new Dictionary<int, IDictionary<int, MapLayerTile>>();

            int row = 0;
            int column = -1;
            foreach (var tile in ReadLayerTiles(reader.ReadSubtree()))
            {
                if (row % width == 0)
                {
                    column++;
                    row = 0;

                    tiles[column] = new Dictionary<int, MapLayerTile>();
                }

                tiles[column][row] = tile;
                row++;
            }

            return new MapLayer(
                layerName, 
                width, 
                height, 
                tiles);
        }

        private IEnumerable<MapLayerTile> ReadLayerTiles(XmlReader reader)
        {
            while (reader.Read())
            {
                if (reader.NodeType != XmlNodeType.Element ||
                    reader.Name != "tile")
                {
                    continue;
                }

                var gid = int.Parse(reader.GetAttribute("gid"), CultureInfo.InvariantCulture);

                yield return new MapLayerTile(gid);
            }
        }

        private Tileset ReadTileset(XmlReader reader)
        {
            var firstGid = int.Parse(reader.GetAttribute("firstgid"), CultureInfo.InvariantCulture);
            var tilesetName = reader.GetAttribute("name");
            var tileWidth = int.Parse(reader.GetAttribute("tilewidth"), CultureInfo.InvariantCulture);
            var tileHeight = int.Parse(reader.GetAttribute("tileheight"), CultureInfo.InvariantCulture);

            List<TilesetImage> images;
            List<TilesetTile> tiles;
            ReadTilesetContent(reader.ReadSubtree(), out images, out tiles);

            return new Tileset(
                firstGid,
                tilesetName,
                tileWidth,
                tileHeight,
                images,
                tiles);
        }

        private void ReadTilesetContent(XmlReader reader, out List<TilesetImage> images, out List<TilesetTile> tiles)
        {
            images = new List<TilesetImage>();
            tiles = new List<TilesetTile>();

            while (reader.Read())
            {
                if (reader.NodeType != XmlNodeType.Element)
                {
                    continue;
                }

                switch (reader.Name)
                {
                    case "image":
                        images.Add(ReadTilesetImage(reader));
                        break;
                    case "tile":
                        tiles.Add(ReadTilesetTile(reader));
                        break;
                    default:
                        break;
                }
            }
        }

        private TilesetImage ReadTilesetImage(XmlReader reader)
        {
            var entrySource = reader.GetAttribute("source");
            var width = int.Parse(reader.GetAttribute("width"), CultureInfo.InvariantCulture);
            var height = int.Parse(reader.GetAttribute("height"), CultureInfo.InvariantCulture);

            return new TilesetImage(
                entrySource,
                width,
                height);
        }

        private TilesetTile ReadTilesetTile(XmlReader reader)
        {
            var id = int.Parse(reader.GetAttribute("id"), CultureInfo.InvariantCulture);
            var properties = ReadTilesetTileProperties(reader.ReadSubtree());

            return new TilesetTile(
                id,
                properties);
        }

        private IEnumerable<KeyValuePair<string, string>> ReadTilesetTileProperties(XmlReader reader)
        {
            while (reader.Read())
            {
                if (reader.NodeType != XmlNodeType.Element ||
                    reader.Name != "property")
                {
                    continue;
                }

                var propertyName = reader.GetAttribute("name");
                var propertyValue = reader.GetAttribute("value");
                
                yield return new KeyValuePair<string, string>(
                    propertyName, 
                    propertyValue);
            }
        }
        #endregion
    }
}
