using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Encounters;
using Assets.Scripts.Maps;
using Assets.Scripts.Triggers.Teleporters;

using UnityEngine;

namespace Assets.Scripts.Actors.Player
{
    public class PlayerBehaviour : MonoBehaviour, ICanTeleport
    {
        #region Methods
        public void Teleport(ITeleportProperties teleportProperties)
        {
            Debug.Log(string.Format("Player wants to teleport to '{0}'.", teleportProperties.MapAssetPath));
            var map = GameObject.Find("Map");

            var loadMapProperties = new LoadMapProperties(teleportProperties.MapAssetPath);
            map.SendMessage(
                "LoadMap",
                loadMapProperties,
                SendMessageOptions.RequireReceiver);
        }
        #endregion
    }
}
