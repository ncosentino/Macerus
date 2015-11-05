using System;
using Assets.Scripts.Components;
using Assets.Scripts.Scenes.Explore;
using ProjectXyz.Application.Core.GameObjects.Loot;
using UnityEngine;

namespace Assets.Scripts.Interactables.Loot
{
    public class LootDropBehaviour : MonoBehaviour
    {
        #region Fields
        private IInteractableBehaviour _interactableBehaviour;

        private IMutableLootDrop _mutableLootDrop;
        #endregion

        #region Methods
        public void Start()
        {
            // FIXME: what the shit is this
            // TODO: we shouldn't be making items in here
            ////var item = ExploreSceneManager.Instance.Manager.DataManager.Items.GetItemById(
            ////    Guid.NewGuid(), 
            ////    ExploreSceneManager.Instance.ItemContext);

            ////_mutableLootDrop = LootDrop.Create(item);
            ////_mutableLootDrop.ItemBeingTaken += MutableLootDrop_ItemBeingTaken;

            ////_interactableBehaviour = this.GetRequiredComponentInChildren<IInteractableBehaviour>();
            ////_interactableBehaviour.Interactable = _mutableLootDrop;
        }
        #endregion

        #region Event Handlers
        private void MutableLootDrop_ItemBeingTaken(object sender, EventArgs e)
        {
            Debug.Log("Took the phat lewtz.");
            Destroy(gameObject);
        }
        #endregion
    }
}
