using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using UnityEngine;

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

                Debug.Log("Got map.");
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

            Debug.Log("Got object layer.");
            return new ObjectLayer(
                layerName, 
                objects);
        }

        private IEnumerable<MapObject> ReadMapObjects(XmlReader reader)
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
                var x = int.Parse(reader.GetAttribute("x"), CultureInfo.InvariantCulture);
                var y = int.Parse(reader.GetAttribute("y"), CultureInfo.InvariantCulture);

                Debug.Log("Got map object.");
                yield return new MapObject(
                    id,
                    objectName,
                    type,
                    gid,
                    x,
                    y);
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

            Debug.Log("Got layer.");
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

                Debug.Log("Got tile.");
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

            Debug.Log("Got tileset.");
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
                        Debug.Log("Unsupported tilset element: " + reader.Name);
                        break;
                }
            }
        }

        private TilesetImage ReadTilesetImage(XmlReader reader)
        {
            var entrySource = reader.GetAttribute("source");
            var width = int.Parse(reader.GetAttribute("width"), CultureInfo.InvariantCulture);
            var height = int.Parse(reader.GetAttribute("height"), CultureInfo.InvariantCulture);

            Debug.Log("Got tileset image.");
            return new TilesetImage(
                entrySource,
                width,
                height);
        }

        private TilesetTile ReadTilesetTile(XmlReader reader)
        {
            var id = int.Parse(reader.GetAttribute("id"), CultureInfo.InvariantCulture);
            var properties = ReadTilesetTileProperties(reader.ReadSubtree());

            Debug.Log("Got tileset tile.");
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
                
                Debug.Log("Got tileset tile property.");
                yield return new KeyValuePair<string, string>(
                    propertyName, 
                    propertyValue);
            }
        }
        #endregion
    }
}
