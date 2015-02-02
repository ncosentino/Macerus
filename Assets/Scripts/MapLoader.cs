using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Tiled;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts
{
    public class MapLoader
    {
        #region Constants
        private const float SPRITE_TO_UNITY_SPACING_MULTIPLIER = 0.32f;
        private const float SPRITE_TO_UNITY_SCALE_MULTIPLIER = SPRITE_TO_UNITY_SPACING_MULTIPLIER + 0.001f;
        #endregion

        #region Fields

        #endregion

        #region Methods
        public void Xxx(GameObject mapObject, ITiledMap map)
        {
            var resources = BuildResources(map.Tilesets, "Assets/Resources/Maps/");

            var tilesContainer = new GameObject()
            {
                name = "Tiles",
            };
            tilesContainer.transform.parent = mapObject.transform;

            Debug.Log("Map Layers: " + map.Layers.Count());
            foreach (var layer in map.Layers)
            {
                var layerContainer = new GameObject();
                layerContainer.transform.parent = tilesContainer.transform;
                layerContainer.name = layer.Name;

                Debug.Log("Layer Size: " + layer.Width + "x" + layer.Heighth);
                for (int columnIndex = 0; columnIndex < layer.Width; columnIndex++)
                {
                    for (int rowIndex = 0; rowIndex < layer.Heighth; rowIndex++)
                    {
                        var tile = layer.GetTile(columnIndex, rowIndex);
                        if (tile.Gid == 0)
                        {
                            continue;
                        }

                        var tileResource = resources[tile.Gid];

                        var tileObject = new GameObject();
                        tileObject.transform.parent = layerContainer.transform;
                        tileObject.AddComponent<SpriteRenderer>();

                        var renderer = tileObject.GetComponent<SpriteRenderer>();
                        renderer.sprite = tileResource.Sprite;
                        renderer.transform.localScale *= SPRITE_TO_UNITY_SCALE_MULTIPLIER;
                        renderer.transform.Translate(
                            columnIndex * SPRITE_TO_UNITY_SPACING_MULTIPLIER,
                            -rowIndex * SPRITE_TO_UNITY_SPACING_MULTIPLIER,
                            0);
                        renderer.sortingLayerName = layerContainer.name;

                        tileObject.name = renderer.sprite.name;

                        ApplyTileProperties(tileObject, tileResource);
                    }
                }
            }
        }

        private void ApplyTileProperties(GameObject tileObject, TilesetTileResource tileResource)
        {
            if (string.Equals("false", tileResource.GetPropertyValue("Walkable"), StringComparison.OrdinalIgnoreCase))
            {
                tileObject.AddComponent<Rigidbody2D>();
                var rigidBody = tileObject.GetComponent<Rigidbody2D>();
                rigidBody.gravityScale = 0;
                rigidBody.fixedAngle = false;
                rigidBody.isKinematic = true;

                tileObject.AddComponent<BoxCollider2D>();
            }
        }

        private Dictionary<int, TilesetTileResource> BuildResources(IEnumerable<Tileset> tilesets, string mapResourceRoot)
        {
            var resources = new Dictionary<int, TilesetTileResource>();

            foreach (var tileset in tilesets)
            {
                int gid = tileset.FirstGid;

                var tilesetImage = tileset.Images.First();
                var resourcePath = CollapsePath(mapResourceRoot + tilesetImage.SourcePath);
                Debug.Log("Resource at: " + resourcePath);

                var sprites = AssetDatabase.LoadAllAssetRepresentationsAtPath(resourcePath)
                    .OfType<Sprite>()
                    .ToArray();

                Debug.Log("Tile count: " + tileset.Tiles.Count());
                foreach (var tilesetTile in tileset.Tiles)
                {
                    Debug.Log("Resource for GID: " + gid);
                    resources[gid++] = new TilesetTileResource(
                        sprites[tilesetTile.Id],
                        tilesetTile.Properties);
                }
            }

            return resources;
        }

        private string CollapsePath(string path)
        {
            var pathSegments = path.Split('/');
            var builder = new StringBuilder();
            string lastSegment = null;

            foreach (var segment in pathSegments)
            {
                if (segment == "..")
                {
                    lastSegment = null;
                    continue;
                }

                if (lastSegment != null)
                {
                    builder.Append(lastSegment + "/");   
                }

                lastSegment = segment;
            }

            if (lastSegment != null)
            {
                builder.Append(lastSegment);
            }

            return builder.ToString();
        }
        #endregion
    }

    public class TilesetTileResource
    {
        #region Fields
        private readonly Sprite _sprite;
        private readonly Dictionary<string, string> _properties;
        #endregion

        #region Constructors
        public TilesetTileResource(Sprite sprite, IEnumerable<KeyValuePair<string, string>> properties)
        {
            _sprite = sprite;
            _properties = new Dictionary<string, string>();

            foreach (var property in properties)
            {
                _properties[property.Key] = property.Value;
            }
        }
        #endregion

        #region Properties
        public Sprite Sprite
        {
            get { return _sprite; }
        }

        public IEnumerable<string> PropertyNames
        {
            get { return _properties.Keys; }
        }
        #endregion

        #region Methods
        public string GetPropertyValue(string propertyName)
        {
            return _properties.ContainsKey(propertyName)
                ? _properties[propertyName]
                : null;
        }
        #endregion
    }
}
