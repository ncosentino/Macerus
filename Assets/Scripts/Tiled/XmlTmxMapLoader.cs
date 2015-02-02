using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using UnityEngine;

namespace Assets.Scripts.Tiled
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
                    tilesets);
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

            int row = -1;
            int column = 0;
            foreach (var tile in ReadLayerTiles(reader.ReadSubtree()))
            {
                if (column % width == 0)
                {
                    row++;
                    column = 0;

                    tiles[row] = new Dictionary<int, MapLayerTile>();
                }

                tiles[row][column] = tile;
                column++;
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

                var gid = reader.GetAttribute("gid");

                Debug.Log("Got tile.");
                yield return new MapLayerTile(gid);
            }
        }

        private Tileset ReadTileset(XmlReader reader)
        {
            var tilesetName = reader.GetAttribute("name");
            var tileWidth = int.Parse(reader.GetAttribute("tilewidth"), CultureInfo.InvariantCulture);
            var tileHeight = int.Parse(reader.GetAttribute("tileheight"), CultureInfo.InvariantCulture);

            var images = ReadTilesetImages(reader.ReadSubtree());
            var tiles = ReadTilesetTiles(reader.ReadSubtree());

            Debug.Log("Got tileset.");
            return new Tileset(
                tilesetName,
                tileWidth,
                tileHeight,
                images,
                tiles);
        }

        private IEnumerable<TilesetImage> ReadTilesetImages(XmlReader reader)
        {
            while (reader.Read())
            {
                if (reader.NodeType != XmlNodeType.Element ||
                    reader.Name != "image")
                {
                    continue;
                }

                var entrySource = reader.GetAttribute("source");
                var width = int.Parse(reader.GetAttribute("width"), CultureInfo.InvariantCulture);
                var height = int.Parse(reader.GetAttribute("height"), CultureInfo.InvariantCulture);

                Debug.Log("Got tileset image.");
                yield return new TilesetImage(
                    entrySource,
                    width,
                    height);
            }
        }

        private IEnumerable<TilesetTile> ReadTilesetTiles(XmlReader reader)
        {
            while (reader.Read())
            {
                if (reader.NodeType != XmlNodeType.Element ||
                    reader.Name != "tile")
                {
                    continue;
                }

                var id = reader.GetAttribute("id");
                var properties = ReadTilesetTileProperties(reader.ReadSubtree());

                Debug.Log("Got tileset tile.");
                yield return new TilesetTile(
                    id,
                    properties);
            }
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
