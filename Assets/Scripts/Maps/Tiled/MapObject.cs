using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Maps.Tiled
{
    public class MapObject : IMapObject
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

        #region Properties
        public string Id
        {
            get { return _id; }
        }

        public string Gid
        {
            get { return _gid; }
        }

        public string Name
        {
            get { return _name; }
        }

        public string Type
        {
            get { return _type; }
        }

        public int X
        {
            get { return _x; }
        }

        public int Y
        {
            get { return _y; }
        }
        #endregion
    }
}
