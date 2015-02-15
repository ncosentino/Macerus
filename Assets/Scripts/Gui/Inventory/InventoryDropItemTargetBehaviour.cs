using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Components;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Gui.Inventory
{
    public class InventoryDropItemTargetBehaviour : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        #region Fields
        private ICanAddItemBehaviour _canAddItemBehaviour;
        #endregion

        #region Unity Properties
        public Image BorderImage;

        public Sprite BorderSprite;

        public Color DefaultBorderColor = Color.white;

        public Color CanDropBorderColor = Color.blue;

        public Color CannotDropBorderColor = Color.red;
        #endregion

        #region Methods
        public void Start()
        {
            _canAddItemBehaviour = this.GetRequiredComponent<ICanAddItemBehaviour>();

            if (BorderImage == null)
            {
                BorderImage = this.GetRequiredComponent<Image>();
            }

            BorderImage.color = DefaultBorderColor;
            BorderImage.sprite = BorderSprite;
        }

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
            if (!_canAddItemBehaviour.CanAddItem(item))
            {
                return;
            }

            draggedItem.Source.CanRemoveItemBehaviour.RemoveItem();
            _canAddItemBehaviour.AddItem(item);
            BorderImage.color = DefaultBorderColor;
        }

        public void OnPointerEnter(PointerEventData eventData)
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
            BorderImage.color = _canAddItemBehaviour.CanAddItem(item)
                ? CanDropBorderColor
                : CannotDropBorderColor;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            BorderImage.color = DefaultBorderColor;
        }
        #endregion
    }
}