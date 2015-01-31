using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Camera
{
    public interface ICameraTargetting
    {
        #region Properties
        Transform CameraTarget { get; }
        #endregion

        #region Methods
        void SetTarget(Transform target);
        #endregion
    }
}
