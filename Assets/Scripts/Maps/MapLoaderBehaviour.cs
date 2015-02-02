using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

using Assets.Scripts.Maps.Tiled;

namespace Assets.Scripts.Maps
{
    public class MapLoaderBehaviour : MonoBehaviour
    {
        #region Constants
        private const float SPRITE_TO_UNITY_SPACING_MULTIPLIER = 0.32f;
        private const float SPRITE_TO_UNITY_SCALE_MULTIPLIER = SPRITE_TO_UNITY_SPACING_MULTIPLIER + 0.001f;
        #endregion

        #region Methods
        public void Start()
        {
            var xmlMapContents = Resources.Load<TextAsset>("Maps/swamp").text;
            var tmxMap = new XmlTmxMapLoader().ReadXml(xmlMapContents);

            var mapPopulator = new TiledMapPopulator(ApplyTileProperties)
            {
                SpriteScaleMultiplier = SPRITE_TO_UNITY_SCALE_MULTIPLIER,
                SpriteSpacingMultiplier = SPRITE_TO_UNITY_SPACING_MULTIPLIER,
                FlipHorizontalPlacement = false,
                FlipVerticalPlacement = true,
            };

            mapPopulator.PopulateMap(gameObject, tmxMap);
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
        #endregion
    }
}