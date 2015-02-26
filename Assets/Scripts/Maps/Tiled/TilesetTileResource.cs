using System;
using System.Collections.Generic;
using System.Linq;
using Tiled.Net.Terrain;
using UnityEngine;

namespace Assets.Scripts.Maps.Tiled
{
    public class TilesetTileResource
    {
        #region Fields
        private readonly Sprite _sprite;
        private readonly Dictionary<string, string> _properties;
        private readonly List<ITerrainType> _cornerTerrains;
        #endregion

        #region Constructors
        public TilesetTileResource(Sprite sprite, IEnumerable<ITerrainType> cornerTerrains, IEnumerable<KeyValuePair<string, string>> properties)
        {
            _sprite = sprite;
            _cornerTerrains = new List<ITerrainType>(cornerTerrains);

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

        public ITerrainType GetCornerTerrainType(int cornerIndex)
        {
            return _cornerTerrains[cornerIndex];
        }
        #endregion
    }
}
