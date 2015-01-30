using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public interface IPlayerControls
    {
        #region Properties
        KeyCode MoveLeft { get; }

        KeyCode MoveRight { get; }

        KeyCode MoveUp { get; }

        KeyCode MoveDown { get; }
        #endregion
    }
}