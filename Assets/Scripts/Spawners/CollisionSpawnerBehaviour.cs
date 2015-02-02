using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Spawners
{
    public class CollisionSpawnerBehaviour : BaseSpawnerBehaviour
    {
        #region Methods
        public override void Start()
        {
            if (SpawnRadius <= 0)
            {
                SpawnRadius = (float) Math.Sqrt(
                    Math.Pow(gameObject.transform.localScale.x, 2) +
                    Math.Pow(gameObject.transform.localScale.y, 2));
            }

            base.Start();
        }

        public void OnTriggerEnter2D(Collider2D collider)
        {
            for (int i = 0; i < TargetSpawnCount; i++)
            {
                Spawner.Spawn(gameObject.transform.parent);
            }
        }
        #endregion
    }
}