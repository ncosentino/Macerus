using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectXyz.Application.Interface.Interactions;

namespace Assets.Scripts.Interactables
{
    public interface ICanInteract
    {
        #region Properties
        IEnumerable<IInteractable> Interactables { get; }
        #endregion
    }
}
