using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Tiled
{
    public class ObjectLayer
    {
        #region Fields
        private readonly string _name;
        private readonly List<MapObject> _objects;
        #endregion

        #region Constructors
        public ObjectLayer(string name, IEnumerable<MapObject> objects)
        {
            _name = name;
            _objects = new List<MapObject>(objects);
        }
        #endregion
    }
}
