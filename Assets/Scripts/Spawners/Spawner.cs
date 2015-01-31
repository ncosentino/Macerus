using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Spawners
{
    public class Spawner
    {
        #region Fields
        private readonly bool _addIndexToName;
        private readonly string _prefabPath;
        private readonly float _spawnRadius;
        private readonly Vector3 _spawnOrigin;
        private readonly string _spawnName;

        private int _currentSpawnCount;
        #endregion

        #region Constructors
        public Spawner(
            string prefabPath, 
            string spawnName, 
            bool addIndexToName, 
            Vector3 spawnOrigin, 
            float spawnRadius)
        {
            _prefabPath = prefabPath;
            _spawnName = spawnName;
            _addIndexToName = addIndexToName;
            _spawnOrigin = spawnOrigin;
            _spawnRadius = spawnRadius;
        }
        #endregion

        #region Properties
        public int CurrentSpawnCount
        {
            get { return _currentSpawnCount; }
        }
        #endregion

        #region Methods
        public void Spawn()
        {
            var spawnAngle = UnityEngine.Random.value * Math.PI * 2;
            var xLocation = (float)(_spawnOrigin.x + _spawnRadius * UnityEngine.Random.value * Math.Cos(spawnAngle));
            var yLocation = (float)(_spawnOrigin.y + _spawnRadius * UnityEngine.Random.value * Math.Sin(spawnAngle));

            var spawned = UnityEngine.Object.Instantiate(
                Resources.Load(_prefabPath), 
                new Vector3(xLocation, yLocation, _spawnOrigin.z),
                Quaternion.identity);
            spawned.name = _addIndexToName
                ? _spawnName + _currentSpawnCount
                : _spawnName;

            _currentSpawnCount++;
        }
        #endregion
    }
}
