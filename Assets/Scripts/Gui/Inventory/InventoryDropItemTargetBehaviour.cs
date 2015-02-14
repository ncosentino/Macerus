using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Components;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Gui.Inventory
{
    public class InventoryDropItemTargetBehaviour : MonoBehaviour, IDropHandler
    {
        #region Fields
        private ICanAddItemBehaviour _canAddItemBehaviour;
        #endregion

        #region Methods
        public void Start()
        {
            _canAddItemBehaviour = this.GetRequiredComponent<ICanAddItemBehaviour>();
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null)
            {
                return;
            }

            var draggedItem = eventData.pointerDrag.GetRequiredComponent<IInventoryDraggedItemBehaviour>();
            if (draggedItem == null || 
                draggedItem.Source == null || 
                draggedItem.Source.GameObject == gameObject)
            {
                return;
            }

            var item = draggedItem.HasItemBehaviour.Item;
            if (_canAddItemBehaviour.CanAddItem(item))
            {
                draggedItem.Source.CanRemoveItemBehaviour.RemoveItem();
                _canAddItemBehaviour.AddItem(item);
            }
        }
        #endregion
    }
}