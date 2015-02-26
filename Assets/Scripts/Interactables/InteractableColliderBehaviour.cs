using System;
using Assets.Scripts.Actors.Player;
using ProjectXyz.Application.Interface.Interactions;
using UnityEngine;

namespace Assets.Scripts.Interactables
{
    public class InteractableColliderBehaviour : MonoBehaviour, IInteractableBehaviour
    {
        #region Properties
        public IInteractable Interactable { get; set; }
        #endregion

        #region Methods
        public void OnTriggerEnter2D(Collider2D collider)
        {
            if (Interactable == null)
            {
                throw new InvalidOperationException("The interactable has not been set.");
            }

            var canInteract = (ICanInteract)collider.gameObject.GetComponent(typeof(ICanInteract));
            if (canInteract == null)
            {
                return;
            }

            canInteract.AddInteractable(Interactable);
        }

        public void OnTriggerExit2D(Collider2D collider)
        {
            if (Interactable == null)
            {
                throw new InvalidOperationException("The interactable has not been set.");
            }

            var canInteract = (ICanInteract)collider.gameObject.GetComponent(typeof(ICanInteract));
            if (canInteract == null)
            {
                return;
            }

            canInteract.RemoveInteractable(Interactable);
        }
        #endregion
    }
}