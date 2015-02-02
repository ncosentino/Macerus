using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Maps.Tiled
{
    public class TiledMapPopulator
    {


        #region Fields
        private readonly Action<GameObject, TilesetTileResource> _transformTile;
        #endregion

        #region Constructors
        public TiledMapPopulator(Action<GameObject, TilesetTileResource> transformTile)
        {
            _transformTile = transformTile;
        }
        #endregion

        #region Properties
        public float SpriteSpacingMultiplier { get; set; }

        public float SpriteScaleMultiplier { get; set; }

        public bool FlipVerticalPlacement { get; set; }

        public bool FlipHorizontalPlacement { get; set; }
        #endregion

        #region Methods
        public void PopulateMap(GameObject mapObject, ITiledMap map)
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
                        if (tile == null || tile.Gid == 0)
                        {
                            continue;
                        }

                        var tileResource = resources[tile.Gid];

                        var tileObject = new GameObject();
                        tileObject.transform.parent = layerContainer.transform;
                        tileObject.AddComponent<SpriteRenderer>();

                        var renderer = tileObject.GetComponent<SpriteRenderer>();
                        renderer.sprite = tileResource.Sprite;
                        renderer.transform.localScale *= SpriteScaleMultiplier;
                        renderer.transform.Translate(
                            columnIndex * SpriteSpacingMultiplier * (FlipHorizontalPlacement ? -1 : 1),
                            rowIndex * SpriteSpacingMultiplier * (FlipVerticalPlacement ? -1 : 1),
                            0);
                        renderer.sortingLayerName = layerContainer.name;

                        tileObject.name = renderer.sprite.name;

                        if (_transformTile != null)
                        {
                            _transformTile(tileObject, tileResource);
                        }
                    }
                }
            }

            Debug.Log("Object Layers: " + map.ObjectLayers.Count());
            foreach (var objectLayer in map.ObjectLayers)
            {
                var objectLayerContainer = new GameObject();
                objectLayerContainer.transform.parent = tilesContainer.transform;
                objectLayerContainer.name = objectLayer.Name;

                foreach (var objectLayerObject in objectLayer.Objects)
                {
                    var prefab = (GameObject)UnityEngine.Object.Instantiate(
                        Resources.Load(objectLayerObject.Type),
                        new Vector3(
                            objectLayerObject.X / 32f * SpriteSpacingMultiplier * (FlipHorizontalPlacement ? -1 : 1),
                            objectLayerObject.Y / 32f * SpriteSpacingMultiplier * (FlipVerticalPlacement ? -1 : 1),
                            0),
                        Quaternion.identity);
                    prefab.transform.parent = objectLayerContainer.transform;
                    prefab.name = objectLayerObject.Name;

                    var renderer = prefab.GetComponent<SpriteRenderer>();
                    if (renderer != null)
                    {
                        renderer.sortingLayerName = objectLayerContainer.name;
                    }
                }
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
}
