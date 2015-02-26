using System;
using UnityEngine;

namespace Assets.Scripts.Interactables.Teleporters
{
    public class CollisionTeleporterBehaviour : MonoBehaviour
    {
        #region Unity Properties
        public float Radius;

        public string TargetMapId;
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
            var teleportProperties = new TeleportProperties(new Guid(TargetMapId));

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