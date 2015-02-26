using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Input
{
    public interface IKeyboardControls
    {
        #region Properties
        KeyCode MoveLeft { get; }

        KeyCode MoveRight { get; }

        KeyCode MoveUp { get; }

        KeyCode MoveDown { get; }

        KeyCode Interact { get; }

        KeyCode ToggleInventory { get; }
        #endregion
    }
}