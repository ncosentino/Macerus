using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Input
{
    public class KeyboardControls : IKeyboardControls
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

        /// <inheritdoc />
        public KeyCode ToggleInventory
        {
            get { return KeyCode.I; }
        }
        #endregion
    }
}
