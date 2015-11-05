using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using UnityEngine;

using Assets.Scripts.Maps.Tiled;
using ProjectXyz.Application.Core.Maps;
using ProjectXyz.Application.Interface.Maps;
using Tiled.Net.Layers;
using Tiled.Net.Parsers;

namespace Assets.Scripts.Maps
{
    public class MapBehaviour : MonoBehaviour, IMapLoader
    {
        #region Constants
        private const float SPRITE_TO_UNITY_SPACING_MULTIPLIER = 0.32f;
        private const float SPRITE_TO_UNITY_SCALE_MULTIPLIER = SPRITE_TO_UNITY_SPACING_MULTIPLIER + 0.001f;

        private static Dictionary<int, int> CORNER_TO_COLLISION_VERTEX = new Dictionary<int, int>()
        {
            { 0, 0 },
            { 1, 2 },
            { 2, 6 },
            { 3, 4 },
        };
        #endregion

        #region Fields
        private IMap _map;
        #endregion

        #region Methods
        public void LoadMap(ILoadMapProperties loadMapProperties)
        {
            _map = loadMapProperties.Manager.ApplicationManager.Maps.GetMapById(
                loadMapProperties.MapId,
                loadMapProperties.MapContext);

            var xmlMapContents = File.ReadAllText(_map.ResourceName);
            var tmxMap = new XmlTmxMapParser().ReadXml(xmlMapContents);

            const string TILE_PREFAB_PATH = "Prefabs/Maps/tile";

            var mapPopulator = new TiledMapPopulator(
                "/Resources/",
                "maps/",
                TILE_PREFAB_PATH,
                ApplyTileProperties,
                ApplyMapObjectProperties)
            {
                SpriteScaleMultiplier = SPRITE_TO_UNITY_SCALE_MULTIPLIER,
                SpriteSpacingMultiplier = SPRITE_TO_UNITY_SPACING_MULTIPLIER,
                FlipHorizontalPlacement = false,
                FlipVerticalPlacement = true,
                CenterAlignGameObjects = true,
            };

            mapPopulator.PopulateMap(gameObject, tmxMap);
        }

        public void Update()
        {
            _map.UpdateElapsedTime(TimeSpan.FromSeconds(Time.deltaTime));

            RenderSettings.ambientLight = new Color(
                _map.AmbientLight.Red,
                _map.AmbientLight.Green,
                _map.AmbientLight.Blue,
                _map.AmbientLight.Alpha);
        }

        private void ApplyTileProperties(GameObject tileObject, TilesetTileResource tileResource)
        {
            var corners = new Vector2[8];
            corners[0] = new Vector2(0, 0);
            corners[1] = new Vector2(0.5f, 0);
            corners[2] = new Vector2(1f, 0);
            corners[3] = new Vector2(1f, -0.5f);
            corners[4] = new Vector2(1f, -1f);
            corners[5] = new Vector2(0.5f, -1f);
            corners[6] = new Vector2(0, -1f);
            corners[7] = new Vector2(0f, -0.5f);

            int walkableCount = 0;
            for (int i = 0; i < 4; i++)
            {
                var terrainType = tileResource.GetCornerTerrainType(i);
                if (terrainType != null && terrainType.Name == "Walkable")
                {
                    corners[CORNER_TO_COLLISION_VERTEX[i]] = new Vector2(0.5f, -0.5f);
                    walkableCount++;
                }
            }

            // if the whole thing is walkable... no collisions please.
            if (walkableCount != 4)
            {
                var rigidBody = tileObject.AddComponent<Rigidbody2D>();
                rigidBody.gravityScale = 0;
                rigidBody.fixedAngle = false;
                rigidBody.isKinematic = true;

                var collider = tileObject.AddComponent<PolygonCollider2D>();
                collider.SetPath(0, corners);
            }
        }

        private void ApplyMapObjectProperties(GameObject prefab, ITiledMapObject tiledMapObject)
        {
            var componentProperties = new Dictionary<string, HashSet<string>>();

            foreach (var propertyName in tiledMapObject.PropertyNames)
            {
                var parts = propertyName.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length == 2)
                {
                    if (!componentProperties.ContainsKey(parts[0]))
                    {
                        componentProperties[parts[0]] = new HashSet<string>();
                    }

                    componentProperties[parts[0]].Add(parts[1]);
                }
            }

            foreach (var componentName in componentProperties.Keys)
            {
                var component = prefab.GetComponent(componentName);
                if (component == null)
                {
                    Debug.LogWarning(string.Format("Could not find component '{0}' on prefab '{1}'.", componentName, prefab.name));
                    continue;
                }

                foreach (var componentProperty in componentProperties[componentName])
                {
                    var field = component.GetType().GetField(componentProperty);
                    if (field == null)
                    {
                        Debug.LogWarning(string.Format("Could not find field '{0}' on component '{1}'.", componentProperty, componentName));
                        continue;
                    }

                    var propertyValue = tiledMapObject.GetPropertyValue(string.Format("{0}_{1}", componentName, componentProperty));
                    field.SetValue(component, propertyValue);    
                }
            }
        }
        #endregion
    }
}