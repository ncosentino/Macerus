using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Triggers.Teleporters
{
    public class TeleportProperties : ITeleportProperties
    {
        #region Fields
        private readonly string _mapAssetPath;
        #endregion

        #region Constructors
        public TeleportProperties(string mapAssetPath)
        {
            _mapAssetPath = mapAssetPath;
        }
        #endregion

        #region Properties
        public string MapAssetPath
        {
            get { return _mapAssetPath; }
        }
        #endregion
    }
}
