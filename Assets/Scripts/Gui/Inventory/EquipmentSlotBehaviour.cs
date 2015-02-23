using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Actors.Player;
using Assets.Scripts.Components;
using Assets.Scripts.Scenes;
using Assets.Scripts.Scenes.Explore;
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Core.Items;
using ProjectXyz.Application.Interface.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Gui.Inventory
{
    public class EquipmentSlotBehaviour : MonoBehaviour, IEquipmentSlotBehaviour
    {
        #region Fields
        private IExploreSceneManager _exploreSceneManager;
        private ICanEquip _equippable;
        private ICanUnequip _unequippable;
        private Func<string, IItem> _getItemForSlotCallback;
        #endregion

        #region Unity Properties
        public string DefaultEquipmentSlotType;

        public Image IconImage;

        public Sprite EmptySprite;
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

            if (IconImage == null)
            {
                IconImage = this.GetRequiredComponent<Image>();
            }

            _exploreSceneManager = ExploreSceneManager.Instance;
            _exploreSceneManager.PlayerBehaviourRegistrar.PlayerRegistered += PlayerBehaviourRegistrar_PlayerRegistered;
            _exploreSceneManager.PlayerBehaviourRegistrar.PlayerUnregistered += PlayerBehaviourRegistrar_PlayerUnregistered;

            if (_exploreSceneManager.PlayerBehaviourRegistrar.PlayerBehaviour != null)
            {
                RegisterPlayer(_exploreSceneManager.PlayerBehaviourRegistrar.PlayerBehaviour);
            }

            IconImage.sprite = EmptySprite;
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

            RefreshItemGraphic(null);
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
            RefreshItemGraphic(item);

            Debug.Log(string.Format("Equipped {0} to {1}.", item, EquipmentSlotType));
        }

        private void OnDestroy()
        {
            _exploreSceneManager.PlayerBehaviourRegistrar.PlayerRegistered -= PlayerBehaviourRegistrar_PlayerRegistered;
            _exploreSceneManager.PlayerBehaviourRegistrar.PlayerUnregistered -= PlayerBehaviourRegistrar_PlayerUnregistered;
        }

        private void RefreshItemGraphic(IItem item)
        {
            IconImage.sprite = item == null
                ? EmptySprite
                : Resources.Load<Sprite>(item.InventoryGraphicResource);
        }

        private void RegisterPlayer(IPlayerBehaviour playerBehaviour)
        {
            _equippable = playerBehaviour.Player;
            _unequippable = playerBehaviour.Player;
            _getItemForSlotCallback = slot => playerBehaviour.Player.Equipment[slot];
            
            // hook events
            playerBehaviour.Player.Equipment.EquipmentChanged += Equipment_EquipmentChanged;

            if (EquipmentSlotType == "Gloves")
            {
                var enchantmentCalculator = EnchantmentCalculator.Create();
                var enchantmentContext = EnchantmentContext.Create();
                var itemContext = ItemContext.Create(enchantmentCalculator, enchantmentContext);
                var item = ExploreSceneManager.Instance.Manager.Items.GetItemById(Guid.NewGuid(), itemContext);

                AddItem(item);
            }
        }
        #endregion

        #region Event Handlers
        private void PlayerBehaviourRegistrar_PlayerRegistered(object sender, PlayerBehaviourRegisteredEventArgs e)
        {
            RegisterPlayer(e.PlayerBehaviour);
        }

        private void PlayerBehaviourRegistrar_PlayerUnregistered(object sender, PlayerBehaviourRegisteredEventArgs e)
        {
            _equippable = null;
            _unequippable = null;
            _getItemForSlotCallback = null;

            // unhook events
            if (e.PlayerBehaviour != null)
            {
                e.PlayerBehaviour.Player.Equipment.EquipmentChanged -= Equipment_EquipmentChanged;
            }
        }

        private void Equipment_EquipmentChanged(object sender, EquipmentChangedEventArgs e)
        {
            if (e.Slot != EquipmentSlotType)
            {
                return;
            }

            RefreshItemGraphic(((IObservableEquipment)sender)[e.Slot]);
        }
        #endregion
    }
}
