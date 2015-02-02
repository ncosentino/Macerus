using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Tiled;
using UnityEngine;

namespace Assets.Scripts
{
    public class DummyMapLoaderBehaviour : MonoBehaviour
    {
        public void Start()
        {
            var mapLoader = new MapLoader();
            var tmxMap = new XmlTmxMapLoader().ReadXml(Resources.Load<TextAsset>("Maps/swamp").text);
            mapLoader.Xxx(gameObject, tmxMap);
        }
    }
}