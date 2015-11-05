using ProjectXyz.Application.Interface.Interactions;
using UnityEngine;

namespace Assets.Scripts.Interactables
{
    public class InteractableBehaviour : MonoBehaviour, IInteractableBehaviour
    {
        #region Properties
        public IInteractable Interactable { get; set; }
        #endregion
    }
}
