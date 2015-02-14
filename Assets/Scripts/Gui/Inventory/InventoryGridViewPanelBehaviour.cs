using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using ProjectXyz.Application.Interface.Items;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Gui.Inventory
{
    public class InventoryGridViewPanelBehaviour : MonoBehaviour, IInventoryGridViewPanelBehaviour
    {
        #region Fields
        private IInventory _inventory;
        #endregion

        #region Unity Properties
        public ScrollRect ItemCollectionScrollRect;

        public GameObject InventorySlotPrefab;

        public float SlotWidth = 64;

        public float SlotHeight = 64;
        #endregion

        #region Properties
        public IInventory Inventory
        {
            get
            {
                return _inventory;
            }

            set
            {
                UnhookInventoryEvents(_inventory);
                _inventory = value;
                HookInventoryEvents(_inventory);
            }
        }
        #endregion

        #region Methods
        private void OnDestroy()
        {
            Inventory = null;
        }
        
        private void HookInventoryEvents(IInventory inventory)
        {
            if (inventory == null)
            {
                return;
            }

            inventory.CapacityChanged += Inventory_CapacityChanged;
            inventory.CollectionChanged += Inventory_CollectionChanged;
        }

        private void UnhookInventoryEvents(IInventory inventory)
        {
            if (inventory == null)
            {
                return;
            }

            inventory.CapacityChanged -= Inventory_CapacityChanged;
            inventory.CollectionChanged -= Inventory_CollectionChanged;
        }

        private void PopulateItemSlots(IMutableInventory inventory, ScrollRect itemCollectionScrollRect)
        {
            (from Transform child 
             in itemCollectionScrollRect.transform
             select child.gameObject)
                .ToList()
                .ForEach(Destroy);
            
            if (inventory == null)
            {
                return;
            }

            int columnCount = (int)(itemCollectionScrollRect.content.rect.width / SlotWidth);
            float stretchedSlotWidth = SlotWidth + (itemCollectionScrollRect.content.rect.width - columnCount * SlotWidth) / columnCount;

            // TODO: this is actually based on the maximum slot...
            int rowCount = (int)Math.Ceiling(Math.Max(inventory.ItemCapacity, inventory.Count) / (float)columnCount);

            for (int x = 0; x < columnCount; x++)
            {
                for (int y = 0; y < rowCount; y++)
                {
                    var slot = (GameObject)PrefabUtility.InstantiatePrefab(InventorySlotPrefab);
                    slot.transform.SetParent(itemCollectionScrollRect.content.transform);

                    var slotRectTransform = slot.GetComponent<RectTransform>();
                    slotRectTransform.SetInsetAndSizeFromParentEdge(
                        RectTransform.Edge.Left,
                        x * stretchedSlotWidth, 
                        0);
                    slotRectTransform.SetInsetAndSizeFromParentEdge(
                        RectTransform.Edge.Top,
                        y * SlotHeight,
                        0);
                    slotRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, stretchedSlotWidth);
                    slotRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, SlotHeight);

                    var inventorySlotBehaviour = (InventorySlotBehaviourBehaviour)slot.GetComponent(typeof(InventorySlotBehaviourBehaviour));
                    inventorySlotBehaviour.Inventory = inventory;
                    inventorySlotBehaviour.InventoryIndex = x + columnCount * y;
                }
            }
        }
        #endregion

        #region Event Handlers
        private void Inventory_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            PopulateItemSlots((IMutableInventory)sender, ItemCollectionScrollRect);
        }

        private void Inventory_CapacityChanged(object sender, EventArgs e)
        {
            PopulateItemSlots((IMutableInventory)sender, ItemCollectionScrollRect);
        }
        #endregion
    }
}