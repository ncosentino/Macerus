using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Components;
using Assets.Scripts.GameObjects;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Gui.Inventory
{
    public class InventoryDragItemSourceBehaviour : MonoBehaviour, IInventoryDragItemSourceBehaviour
    {
        #region Fields
        private ICanRemoveItemBehaviour _canRemoveItemBehaviour;
        private IHasItemBehaviour _hasItemBehaviour;
        #endregion

        #region Unity Properties
        public Image IconImage;

        public GameObject DraggingItemPrefab;
        #endregion

        #region Properties
        public GameObject GameObject
        {
            get { return gameObject; }
        }

        public ICanRemoveItemBehaviour CanRemoveItemBehaviour
        {
            get { return _canRemoveItemBehaviour; }
        }
        #endregion

        #region Methods
        public void Start()
        {
            _canRemoveItemBehaviour = this.GetRequiredComponent<ICanRemoveItemBehaviour>();
            _hasItemBehaviour = this.GetRequiredComponent<IHasItemBehaviour>();

            if (DraggingItemPrefab == null)
            {
                throw new InvalidOperationException("The prefab for the dragging item must be set.");
            }

            if (!DraggingItemPrefab.HasRequiredComponent<IInventoryDraggedItemBehaviour>())
            {
                throw new InvalidOperationException(string.Format("The prefab for the dragging must have '{0}' as a component.", typeof(IInventoryDraggedItemBehaviour)));
            }

            if (IconImage == null)
            {
                IconImage = this.GetRequiredComponent<Image>();
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!_canRemoveItemBehaviour.CanRemoveItem())
            {
                return;
            }

            var dragItem = (GameObject)PrefabUtility.InstantiatePrefab(DraggingItemPrefab);
            dragItem.name = "Dragged Item";
            dragItem.transform.SetParent(this.GetRequiredComponentInParent<Canvas>().transform);
            dragItem.transform.position = eventData.position;
            dragItem.transform.Translate(new Vector3(64, -64));

            var draggedItemBehaviour = dragItem.GetRequiredComponent<IInventoryDraggedItemBehaviour>();
            draggedItemBehaviour.Source = this;
            draggedItemBehaviour.HasItemBehaviour = _hasItemBehaviour;
            draggedItemBehaviour.Icon = IconImage.sprite;

            eventData.pointerDrag = dragItem;
        }

        public void OnDrag(PointerEventData eventData)
        {
        }
        #endregion
    }
}