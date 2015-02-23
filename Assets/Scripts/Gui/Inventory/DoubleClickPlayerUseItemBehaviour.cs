using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Actors.Player;
using Assets.Scripts.Scenes.Explore;
using ProjectXyz.Application.Interface.Items;
using UnityEngine;

namespace Assets.Scripts.Gui.Inventory
{
    public class DoubleClickPlayerUseItemBehaviour : DoubleClickUseItemBehaviour
    {
        #region Fields
        private ICanUseItem _target;
        private IExploreSceneManager _exploreSceneManager;
        #endregion

        #region Properties
        protected override ICanUseItem Target
        {
            get { return _target; }
        }
        #endregion

        #region Methods
        public override void Start()
        {
            base.Start();

            _exploreSceneManager = ExploreSceneManager.Instance;
            _exploreSceneManager.PlayerBehaviourRegistrar.PlayerRegistered += PlayerBehaviourRegistrar_PlayerRegistered;
            _exploreSceneManager.PlayerBehaviourRegistrar.PlayerUnregistered += PlayerBehaviourRegistrar_PlayerUnregistered;
            RegisterPlayerBehaviour(_exploreSceneManager.PlayerBehaviourRegistrar.PlayerBehaviour);
        }

        private void OnDestroy()
        {
            if (_exploreSceneManager != null)
            {
                _exploreSceneManager.PlayerBehaviourRegistrar.PlayerRegistered -= PlayerBehaviourRegistrar_PlayerRegistered;
                _exploreSceneManager.PlayerBehaviourRegistrar.PlayerUnregistered -= PlayerBehaviourRegistrar_PlayerUnregistered;
                _exploreSceneManager = null;
            }
        }

        private void RegisterPlayerBehaviour(IPlayerBehaviour playerBehaviour)
        {
            _target = playerBehaviour.Player;
        }
        #endregion

        #region Event Handlers
        private void PlayerBehaviourRegistrar_PlayerRegistered(object sender, PlayerBehaviourRegisteredEventArgs e)
        {
            RegisterPlayerBehaviour(e.PlayerBehaviour);
        }

        private void PlayerBehaviourRegistrar_PlayerUnregistered(object sender, PlayerBehaviourRegisteredEventArgs e)
        {
            _target = null;
        }
        #endregion
    }
}
