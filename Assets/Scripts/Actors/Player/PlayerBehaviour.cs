﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Encounters;
using Assets.Scripts.Maps;
using Assets.Scripts.Scenes;
using Assets.Scripts.Triggers.Teleporters;

using UnityEngine;

namespace Assets.Scripts.Actors.Player
{
    public class PlayerBehaviour : MonoBehaviour, IPlayerBehaviour
    {
        #region Unity Properties
        public string Id;
        #endregion

        #region Properties
        public GameObject PlayerGameObject
        {
            get { return gameObject; }
        }
        #endregion

        #region Methods
        public void Start()
        {
            ExploreSceneManager.Instance.RegisterPlayer(this);
        }

        public void Teleport(ITeleportProperties teleportProperties)
        {
            Debug.Log(string.Format("Player wants to teleport to '{0}'.", teleportProperties.MapAssetPath));
            
            var loadMapProperties = new LoadMapProperties(teleportProperties.MapAssetPath);
            ExploreSceneManager.Instance.LoadMap(loadMapProperties);
        }

        public void Encounter(IEncounterProperties encounterProperties)
        {
            Debug.Log("Player has encountered something!!!");
        }
        #endregion
    }
}
