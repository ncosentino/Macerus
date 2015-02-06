using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace Assets.Scripts.Triggers.Teleporters
{
    public class CollisionTeleporterBehaviour : MonoBehaviour
    {
        #region UnityFields
        public float Radius;

        public string MapAssetPath;
        #endregion

        #region Methods
        public void Start()
        {
            if (Radius <= 0)
            {
                Radius = (float)Math.Sqrt(
                    Math.Pow(gameObject.transform.localScale.x / 2, 2) +
                    Math.Pow(gameObject.transform.localScale.y / 2, 2));
            }
        }

        public void OnTriggerEnter2D(Collider2D collider)
        {
            var teleportProperties = new TeleportProperties(MapAssetPath);

            var teleporter = (ICanTeleport)collider.gameObject.GetComponent(typeof(ICanTeleport));
            if (teleporter == null)
            {
                return;
            }

            teleporter.Teleport(teleportProperties);
        }
        #endregion
    }
}