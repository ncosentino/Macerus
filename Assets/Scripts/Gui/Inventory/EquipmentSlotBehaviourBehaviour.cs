using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Actors.Player;
using Assets.Scripts.Scenes;
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Core.Items;
using ProjectXyz.Application.Interface.Items;
using UnityEngine;

namespace Assets.Scripts.Gui.Inventory
{
    public class EquipmentSlotBehaviourBehaviour : MonoBehaviour, IEquipmentSlotBehaviourBehaviour
    {
        #region Fields
        private IExploreSceneManager _exploreSceneManager;
        private ICanEquip _equippable;
        private ICanUnequip _unequippable;
        private Func<string, IItem> _getItemForSlotCallback;
        #endregion

        #region Unity Properties
        public string DefaultEquipmentSlotType;
        #endregion
        
        #region Properties
        public string EquipmentSlotType { get; private set; }

        public IItem Item
        {
            get
            {
                return _getItemForSlotCallback == null
                    ? null
                    : _getItemForSlotCallback(EquipmentSlotType);
            }
        }
        #endregion

        #region Methods
        public void Start()
        {
            if (string.IsNullOrEmpty(EquipmentSlotType))
            {
                EquipmentSlotType = DefaultEquipmentSlotType;
            }

            _exploreSceneManager = ExploreSceneManager.Instance;
            _exploreSceneManager.PlayerBehaviourRegistrar.PlayerRegistered += PlayerBehaviourRegistrar_PlayerRegistered;
            _exploreSceneManager.PlayerBehaviourRegistrar.PlayerUnregistered += PlayerBehaviourRegistrar_PlayerUnregistered;
        }

        public void OnDestroy()
        {
            _exploreSceneManager.PlayerBehaviourRegistrar.PlayerRegistered -= PlayerBehaviourRegistrar_PlayerRegistered;
            _exploreSceneManager.PlayerBehaviourRegistrar.PlayerUnregistered -= PlayerBehaviourRegistrar_PlayerUnregistered;
        }

        public bool CanRemoveItem()
        {
            var result = _unequippable.CanUnequip(EquipmentSlotType);
            Debug.Log(string.Format("Can unequip from {0}? {1}", EquipmentSlotType, result));

            return result;
        }

        public IItem RemoveItem()
        {
            var item = _unequippable.Unequip(EquipmentSlotType);
            Debug.Log(string.Format("Unequipped {0} from {1}.", item, EquipmentSlotType));

            return item;
        }

        public bool CanAddItem(IItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("Cannot add null item.");
            }

            var result = _equippable.CanEquip(item, EquipmentSlotType);
            Debug.Log(string.Format("Can {0} equip to {1}? {2}", item, EquipmentSlotType, result));

            return result;
        }

        public void AddItem(IItem item)
        {
            _equippable.Equip(item, EquipmentSlotType);
            Debug.Log(string.Format("Equipped {0} to {1}.", item, EquipmentSlotType));
        }
        #endregion

        #region Event Handlers
        private void PlayerBehaviourRegistrar_PlayerRegistered(object sender, PlayerBehaviourRegisteredEventArgs e)
        {
            _equippable = e.PlayerBehaviour.Player;
            _unequippable = e.PlayerBehaviour.Player;
            _getItemForSlotCallback = slot => e.PlayerBehaviour.Player.Equipment[slot];

            if (EquipmentSlotType == "Gloves")
            {
                var enchantmentCalculator = EnchantmentCalculator.Create();
                var enchantmentContext = EnchantmentContext.Create();
                var itemContext = ItemContext.Create(enchantmentCalculator, enchantmentContext);
                var item = ExploreSceneManager.Instance.Manager.Items.GetItemById(Guid.NewGuid(), itemContext);

                AddItem(item);
            }
        }

        private void PlayerBehaviourRegistrar_PlayerUnregistered(object sender, PlayerBehaviourRegisteredEventArgs e)
        {
            _equippable = null;
            _unequippable = null;
            _getItemForSlotCallback = null;
        }
        #endregion
    }
}
