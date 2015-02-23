using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Encounters;
using Assets.Scripts.Maps;
using Assets.Scripts.Scenes;
using Assets.Scripts.Scenes.Explore;
using Assets.Scripts.Triggers.Teleporters;
using ProjectXyz.Application.Core.Actors;
using ProjectXyz.Application.Interface.Actors;
using UnityEngine;

namespace Assets.Scripts.Actors.Player
{
    public class PlayerBehaviour : ActorBehaviour, IPlayerBehaviour
    {
        #region Properties
        public IActor Player
        {
            get { return Actor; }
        }
        #endregion

        #region Methods
        public override void Start()
        {
            base.Start();

            ExploreSceneManager.Instance.PlayerBehaviourRegistrar.RegisterPlayer(this);
        }

        public void Teleport(ITeleportProperties teleportProperties)
        {
            Debug.Log(string.Format("Player wants to teleport to '{0}'.", teleportProperties.MapId));
            
            ExploreSceneManager.Instance.LoadMap(teleportProperties.MapId);
        }

        public void Encounter(IEncounterProperties encounterProperties)
        {
            Debug.Log("Player has encountered something!!!");
        }
        #endregion
    }
}
