using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Actors.Player;
using Assets.Scripts.Scenes;
using UnityEngine;

namespace Assets.Scripts.Gui.Inventory
{
    public sealed class PlayerInventoryGridViewPanelBehaviour : InventoryGridViewPanelBehaviour
    {
        #region Fields
        private IExploreSceneManager _exploreSceneManager;
        #endregion
        
        #region Methods
        public void Start()
        {
            _exploreSceneManager = ExploreSceneManager.Instance;
            _exploreSceneManager.PlayerBehaviourRegistrar.PlayerRegistered += PlayerBehaviourRegistrar_PlayerRegistered;
            _exploreSceneManager.PlayerBehaviourRegistrar.PlayerUnregistered += PlayerBehaviourRegistrar_PlayerUnregistered;
        }

        private void OnDestroy()
        {
            _exploreSceneManager.PlayerBehaviourRegistrar.PlayerRegistered -= PlayerBehaviourRegistrar_PlayerRegistered;
            _exploreSceneManager.PlayerBehaviourRegistrar.PlayerUnregistered -= PlayerBehaviourRegistrar_PlayerUnregistered;
        }
        #endregion

        #region Event Handlers
        private void PlayerBehaviourRegistrar_PlayerUnregistered(object sender, PlayerBehaviourRegisteredEventArgs e)
        {
            Inventory = null;
        }

        private void PlayerBehaviourRegistrar_PlayerRegistered(object sender, PlayerBehaviourRegisteredEventArgs e)
        {
            Inventory = e.PlayerBehaviour.Player.Inventory;
        }
        #endregion
    }
}