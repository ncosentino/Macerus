using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Spawners
{
    public class BaseSpawnerBehaviour : MonoBehaviour
    {
        #region Fields
        private int _targetSpawnCount;
        private Spawner _spawner;
        #endregion

        #region Unity Properties
        public float ChanceToExist = 1f;

        public string PrefabPath;

        public int MinimumSpawns = 1;

        public int MaximumSpawns = 1;

        public float SpawnRadius = 0;

        public string SpawnName;
        #endregion

        #region Properties
        protected Spawner Spawner
        {
            get { return _spawner; }
        }

        protected int TargetSpawnCount
        {
            get { return _targetSpawnCount; }
        }

        protected bool Destroyed { get; private set; }
        #endregion

        #region Methods
        public virtual void Start()
        {
            if (string.IsNullOrEmpty(PrefabPath))
            {
                throw new ArgumentException("The prefab path must be set.");
            }

            if (UnityEngine.Random.value > ChanceToExist)
            {
                Destroy(gameObject);
                return;
            }

            _targetSpawnCount = UnityEngine.Random.Range(MinimumSpawns, MaximumSpawns);
            
            _spawner = new Spawner(
                PrefabPath,
                SpawnName,
                MaximumSpawns == 1,
                gameObject.transform.position,
                SpawnRadius);
        }

        public virtual void Update()
        {
            // check if we've exceeded our limit
            if (TargetSpawnCount <= Spawner.CurrentSpawnCount)
            {
                Destroy(gameObject);
                Destroyed = true;
            }
        }
        #endregion
    }
}