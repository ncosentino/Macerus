using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Components;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Items.ExtensionMethods;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Gui.Inventory
{
    public class InventoryGridViewPanelBehaviour : MonoBehaviour, IInventoryGridViewPanelBehaviour
    {
        #region Fields
        private IMutableInventory _inventory;
        #endregion

        #region Unity Properties
        public RectTransform ScrollRectContent;

        public GameObject InventorySlotPrefab;

        public float SlotWidth = 64;

        public float SlotHeight = 64;
        #endregion

        #region Properties
        public IMutableInventory Inventory
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

                PopulateItemSlots(_inventory, ScrollRectContent);
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
        }

        private void UnhookInventoryEvents(IInventory inventory)
        {
            if (inventory == null)
            {
                return;
            }

            inventory.CapacityChanged -= Inventory_CapacityChanged;
        }

        private void PopulateItemSlots(IMutableInventory inventory, RectTransform scrollRectContent)
        {
            if (scrollRectContent == null)
            {
                return;
            }

            (from Transform child
             in scrollRectContent
             select child.gameObject)
                .ToList()
                .ForEach(Destroy);
            
            if (inventory == null)
            {
                return;
            }

            int columnCount = (int)(scrollRectContent.rect.width / SlotWidth);
            float stretchedSlotWidth = SlotWidth + (scrollRectContent.rect.width - columnCount * SlotWidth) / columnCount;

            int lastUsedSlot;
            inventory.TryGetLastUsedSlot(out lastUsedSlot);

            int rowCount = (int)Math.Ceiling(Math.Max(inventory.ItemCapacity, lastUsedSlot) / (float)columnCount);

            Debug.Log(string.Format("Inventory: {0}x{1}", columnCount, rowCount));

            for (int y = 0; y < rowCount; y++) 
            {
                for (int x = 0; x < columnCount; x++)
                {
                    var slot = (GameObject)PrefabUtility.InstantiatePrefab(InventorySlotPrefab);
                    slot.transform.SetParent(scrollRectContent);

                    var slotRectTransform = slot.GetRequiredComponent<RectTransform>();
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

                    var inventorySlotBehaviour = slot.GetRequiredComponent<IInventorySlotBehaviour>();
                    inventorySlotBehaviour.Inventory = inventory;
                    inventorySlotBehaviour.InventoryIndex = x + columnCount * y;
                }
            }

            scrollRectContent.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rowCount * SlotHeight);
        }
        #endregion

        #region Event Handlers
        private void Inventory_CapacityChanged(object sender, EventArgs e)
        {
            PopulateItemSlots((IMutableInventory)sender, ScrollRectContent);
        }
        #endregion
    }
}