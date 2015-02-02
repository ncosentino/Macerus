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
        private const float SPRITE_TO_UNITY_MULTIPLIER = 0.32f;
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

                        var tileObject = new GameObject();
                        tileObject.transform.parent = layerContainer.transform;
                        tileObject.AddComponent<SpriteRenderer>();

                        var renderer = tileObject.GetComponent<SpriteRenderer>();
                        renderer.sprite = resources[tile.Gid];
                        renderer.transform.localScale *= SPRITE_TO_UNITY_MULTIPLIER;
                        renderer.transform.Translate(
                            columnIndex * SPRITE_TO_UNITY_MULTIPLIER,
                            -rowIndex * SPRITE_TO_UNITY_MULTIPLIER,
                            0);
                        renderer.sortingLayerName = layerContainer.name;

                        tileObject.name = renderer.sprite.name;
                    }
                }
            }
        }

        private Dictionary<int, Sprite> BuildResources(IEnumerable<Tileset> tilesets, string mapResourceRoot)
        {
            var resources = new Dictionary<int, Sprite>();

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
                    resources[gid++] = sprites[tilesetTile.Id];   
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
}
