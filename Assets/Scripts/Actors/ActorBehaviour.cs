using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Encounters;
using Assets.Scripts.Maps;
using Assets.Scripts.Scenes;
using Assets.Scripts.Scenes.Explore;
using ProjectXyz.Application.Core.Actors;
using ProjectXyz.Application.Interface.Actors;
using UnityEngine;

namespace Assets.Scripts.Actors
{
    public class ActorBehaviour : MonoBehaviour, IActorBehaviour
    {
        #region Properties
        public GameObject ActorGameObject
        {
            get { return gameObject; }
        }

        public IActor Actor { get; private set; }
        #endregion

        #region Methods
        public virtual void Start()
        {
            Actor = ExploreSceneManager.Instance.Manager.Actors.GetActorById(
                Guid.NewGuid(),
                ExploreSceneManager.Instance.ActorContext);
        }

        public virtual void Update()
        {
            Actor.UpdateElapsedTime(TimeSpan.FromSeconds(Time.deltaTime));
        }
        #endregion
    }
}
