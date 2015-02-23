using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Actors.Player;
using Assets.Scripts.Components;
using Assets.Scripts.Scenes;
using Assets.Scripts.Scenes.Explore;
using ProjectXyz.Application.Core.Actors.ExtensionMethods;
using ProjectXyz.Application.Interface.Actors;
using UnityEngine;

namespace Assets.Scripts.Gui.Hud
{
    public class HealthBarBehaviour : MonoBehaviour
    {
        #region Constants
        private const float DEFAULT_UPDATE_INTERVAL = 0.25f;
        #endregion

        #region Fields
        private IExploreSceneManager _exploreSceneManager;
        private ICircularFillControlBehaviour _circularFillControlBehaviour;
        #endregion

        #region Unity Properties
        public float UpdateInterval = DEFAULT_UPDATE_INTERVAL;
        #endregion

        #region Methods
        public void Start()
        {
            if (UpdateInterval <= 0)
            {
                UpdateInterval = DEFAULT_UPDATE_INTERVAL;
            }

            _exploreSceneManager = ExploreSceneManager.Instance;
            _circularFillControlBehaviour = this.GetRequiredComponent<ICircularFillControlBehaviour>();

            _exploreSceneManager.PlayerBehaviourRegistrar.PlayerRegistered += PlayerBehaviourRegistrar_PlayerRegistered;
            _exploreSceneManager.PlayerBehaviourRegistrar.PlayerUnregistered += PlayerBehaviourRegistrar_PlayerUnregistered;

            if (_exploreSceneManager.PlayerBehaviourRegistrar.PlayerBehaviour != null)
            {
                StartCoroutine(UpdateHealthBar(_exploreSceneManager.PlayerBehaviourRegistrar.PlayerBehaviour.Player));    
            }
        }

        private void OnDestroy()
        {
            StopCoroutine(UpdateHealthBar(null));

            _exploreSceneManager.PlayerBehaviourRegistrar.PlayerRegistered -= PlayerBehaviourRegistrar_PlayerRegistered;
            _exploreSceneManager.PlayerBehaviourRegistrar.PlayerUnregistered -= PlayerBehaviourRegistrar_PlayerUnregistered;
        }

        public IEnumerator UpdateHealthBar(IActor player)
        {
            while (true)
            {
                _circularFillControlBehaviour.Value = (float)(player.GetCurrentLife() / player.GetMaximumLife());
                yield return new WaitForSeconds(UpdateInterval);
            }
        }
        #endregion

        #region Event Handlers
        private void PlayerBehaviourRegistrar_PlayerRegistered(object sender, PlayerBehaviourRegisteredEventArgs e)
        {
            StartCoroutine(UpdateHealthBar(e.PlayerBehaviour.Player));
        }

        private void PlayerBehaviourRegistrar_PlayerUnregistered(object sender, PlayerBehaviourRegisteredEventArgs e)
        {
            StopCoroutine(UpdateHealthBar(e.PlayerBehaviour == null ? null : e.PlayerBehaviour.Player));
        }
        #endregion
    }
}