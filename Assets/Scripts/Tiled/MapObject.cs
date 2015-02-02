using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Tiled
{
    public class MapObject
    {
        #region Fields
        private readonly string _id;
        private readonly string _gid;
        private readonly string _name;
        private readonly string _type;
        private readonly int _x;
        private readonly int _y;
        #endregion

        #region Constructors
        public MapObject(string id, string name, string type, string gid, int x, int y)
        {
            _id = id;
            _name = name;
            _type = type;
            _gid = gid;
            _x = x;
            _y = y;
        }
        #endregion
    }
}
