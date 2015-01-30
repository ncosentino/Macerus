using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerControls : MonoBehaviour, IPlayerControls
    {
        #region Properties
        /// <inheritdoc />
        public KeyCode MoveLeft
        {
            get { return KeyCode.A; }
        }

        /// <inheritdoc />
        public KeyCode MoveRight
        {
            get { return KeyCode.D; }
        }

        /// <inheritdoc />
        public KeyCode MoveUp
        {
            get { return KeyCode.W; }
        }

        /// <inheritdoc />
        public KeyCode MoveDown
        {
            get { return KeyCode.S; }
        }
        #endregion
    }
}
