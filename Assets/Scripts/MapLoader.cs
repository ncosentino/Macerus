using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using UnityEngine;

namespace Assets.Scripts
{
    public class MapLoader : MonoBehaviour
    {
        #region Fields
        private object _map;
        #endregion

        #region Methods
        public void Start()
        {
            ////using (var reader = XmlReader.Create(new StringReader(Resources.Load<TextAsset>("Maps/Swamp").text)))
            ////{
                //// TODO: fill this out!
            ////}
        }
        #endregion
    }
}
