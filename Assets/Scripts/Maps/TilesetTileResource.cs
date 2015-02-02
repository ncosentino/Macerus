using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Maps
{
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
