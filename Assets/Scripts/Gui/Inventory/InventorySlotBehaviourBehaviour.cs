using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Actors.Player;
using Assets.Scripts.Scenes;
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Core.Items;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Items.ExtensionMethods;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Gui.Inventory
{
    public class InventorySlotBehaviourBehaviour : MonoBehaviour, ICanAddItemBehaviour, ICanRemoveItemBehaviour, IHasItemBehaviour
    {
        #region Fields
        private IExploreSceneManager _exploreSceneManager;
        private ICanEquip _equippable;
        private ICanUnequip _unequippable;
        private Func<string, IItem> _getItemForSlotCallback;
        #endregion
        
        #region Properties
        public int InventoryIndex { get; set; }

        public IMutableInventory Inventory { get; set; }

        public IItem Item
        {
            get { return Inventory.Items.ToList()[InventoryIndex]; }
        }
        #endregion

        #region Methods
        public void Start()
        {
        }

        public void OnDestroy()
        {
           
        }

        public bool CanRemoveItem()
        {
            var result = Inventory.Items.Count > InventoryIndex;
            Debug.Log(string.Format("Can remove from {0}? {1}", InventoryIndex, result));

            return result;
        }

        public IItem RemoveItem()
        {
            var item = Item;
            Inventory.Remove(Item);
            Debug.Log(string.Format("Removed {0} from {1}.", item, InventoryIndex));

            return item;
        }

        public bool CanAddItem(IItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("Cannot add null item.");
            }

            var result = Inventory.Items.Count < Inventory.ItemCapacity;
            Debug.Log(string.Format("Can {0} be added? {1}", item, result));

            return result;
        }

        public void AddItem(IItem item)
        {
            Inventory.Add(item);
            Debug.Log(string.Format("Added {0}.", item));
        }
        #endregion
    }
}
