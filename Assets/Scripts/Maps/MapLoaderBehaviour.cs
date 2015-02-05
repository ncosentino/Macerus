using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using UnityEngine;

using Assets.Scripts.Maps.Tiled;
using Tiled.Net.Layers;
using Tiled.Net.Parsers;

namespace Assets.Scripts.Maps
{
    public class MapLoaderBehaviour : MonoBehaviour
    {
        #region Constants
        private const float SPRITE_TO_UNITY_SPACING_MULTIPLIER = 0.32f;
        private const float SPRITE_TO_UNITY_SCALE_MULTIPLIER = SPRITE_TO_UNITY_SPACING_MULTIPLIER + 0.001f;
        #endregion

        #region Unity Properties
        public string MapAssetPath;
        #endregion

        #region Methods
        public void Start()
        {
            var xmlMapContents = File.ReadAllText(MapAssetPath);
            var tmxMap = new XmlTmxMapParser().ReadXml(xmlMapContents);

            var mapPopulator = new TiledMapPopulator(
                "/Resources/",
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

            // this is a one shot component
            Destroy(this);
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