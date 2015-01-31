using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class SpawnerBehaviour : MonoBehaviour
    {
        #region Fields
        private float _remainingTime;
        private int _targetSpawnCount;
        private int _currentSpawnCount;
        #endregion

        #region Unity Properties
        public float ChanceToExist = 1f;

        public string PrefabPath;

        public int MinimumSpawns = 1;

        public int MaximumSpawns = 1;

        public float Interval;

        public float SpawnRadius = 0;

        public string SpawnName;
        #endregion

        #region Methods
        public void Start()
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
            _remainingTime = Interval;
        }

        public void Update()
        {      
            // check if we've exceeded our limit
            if (_targetSpawnCount <= _currentSpawnCount)
            {
                Destroy(gameObject);
                return;
            }

            _remainingTime -= Time.deltaTime;

            if (_remainingTime <= 0)
            {
                
                var spawnAngle = UnityEngine.Random.value * Math.PI * 2;
                var xLocation = (float)(gameObject.transform.position.x + SpawnRadius * UnityEngine.Random.value * Math.Cos(spawnAngle));
                var yLocation = (float)(gameObject.transform.position.y + SpawnRadius * UnityEngine.Random.value * Math.Sin(spawnAngle));

                var spawned = Instantiate(Resources.Load(PrefabPath), new Vector3(xLocation, yLocation, gameObject.transform.position.z), Quaternion.identity);
                spawned.name = MaximumSpawns == 1
                    ? SpawnName
                    : SpawnName + _currentSpawnCount;
                
                _currentSpawnCount++;
                _remainingTime = Interval;
            }
        }
        #endregion
    }
}