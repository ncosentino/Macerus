using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Spawners
{
    public class IntervalSpawnerBehaviour : BaseSpawnerBehaviour
    {
        #region Fields
        private float _remainingTime;
        #endregion

        #region Unity Properties
        public float Interval;
        #endregion

        #region Methods
        public override void Start()
        {
            _remainingTime = Interval;
            base.Start();
        }

        public override void Update()
        {
            base.Update();
            if (Destroyed)
            {
                return;
            }

            _remainingTime -= Time.deltaTime;

            if (_remainingTime <= 0)
            {
                Spawner.Spawn(gameObject.transform.parent);
                _remainingTime = Interval;
            }
        }
        #endregion
    }
}