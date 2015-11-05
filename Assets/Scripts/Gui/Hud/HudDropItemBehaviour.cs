using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Gui.Inventory;
using Assets.Scripts.Scenes.Explore;
using ProjectXyz.Application.Interface.Items;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Gui.Hud
{
    public class HudDropItemBehaviour : MonoBehaviour, IDropHandler, ICanAddItemBehaviour
    {
        #region Methods
        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null)
            {
                return;
            }

            var draggedItem = (IInventoryDraggedItemBehaviour)eventData.pointerDrag.GetComponent(typeof(IInventoryDraggedItemBehaviour));
            if (draggedItem == null || 
                draggedItem.Source == null || 
                draggedItem.Source.GameObject == gameObject)
            {
                return;
            }

            var item = draggedItem.HasItemBehaviour.Item;
            if (!CanAddItem(item))
            {
                return;
            }

            draggedItem.Source.CanRemoveItemBehaviour.RemoveItem();
            AddItem(item);
        }

        public bool CanAddItem(IItem item)
        {
            return true;
        }

        public void AddItem(IItem item)
        {
            var dropLocation = ExploreSceneManager.Instance.PlayerBehaviourRegistrar.PlayerBehaviour.ActorGameObject.transform.position;

            throw new NotImplementedException();
            ////var lootDrop = ExploreSceneManager.Instance.GameObjectFactory.CreateLootDrop(item);

            ////ExploreSceneManager.Instance.Map.AddGameObject(lootDrop, dropLocation);
            ////Debug.Log("Bye, Felicia.");
        }
        #endregion
    }
}
