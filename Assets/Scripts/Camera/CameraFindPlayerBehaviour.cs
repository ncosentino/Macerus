using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Actors.Player;
using Assets.Scripts.Scenes;
using UnityEngine;

namespace Assets.Scripts.Camera
{
    [RequireComponent(typeof(ICameraTargetting))]
    public sealed class CameraFindPlayerBehaviour : MonoBehaviour
    {
        #region Fields
        private ICameraTargetting _cameraTargetting;
        private IExploreSceneManager _exploreSceneManager;
        #endregion
        
        #region Methods
        public void Start()
        {
            _cameraTargetting = (ICameraTargetting)gameObject.GetComponent(typeof(ICameraTargetting));

            _exploreSceneManager = ExploreSceneManager.Instance;
            _exploreSceneManager.PlayerBehaviourRegistrar.PlayerRegistered += PlayerBehaviourRegistrar_PlayerRegistered;
            _exploreSceneManager.PlayerBehaviourRegistrar.PlayerUnregistered += PlayerBehaviourRegistrar_PlayerUnregistered;
        }

        public void OnDestroy()
        {
            _exploreSceneManager.PlayerBehaviourRegistrar.PlayerRegistered -= PlayerBehaviourRegistrar_PlayerRegistered;
            _exploreSceneManager.PlayerBehaviourRegistrar.PlayerUnregistered -= PlayerBehaviourRegistrar_PlayerUnregistered;
        }
        #endregion

        #region Event Handlers
        private void PlayerBehaviourRegistrar_PlayerRegistered(object sender, PlayerBehaviourRegisteredEventArgs e)
        {
            _cameraTargetting.SetTarget(e.PlayerBehaviour.PlayerGameObject.transform);
        }

        private void PlayerBehaviourRegistrar_PlayerUnregistered(object sender, PlayerBehaviourRegisteredEventArgs e)
        {
            _cameraTargetting.SetTarget(null);
        }
        #endregion
    }
}
