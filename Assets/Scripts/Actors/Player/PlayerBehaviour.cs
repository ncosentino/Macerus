using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Encounters;
using Assets.Scripts.Interactables;
using Assets.Scripts.Interactables.Teleporters;
using Assets.Scripts.Maps;
using Assets.Scripts.Scenes;
using Assets.Scripts.Scenes.Explore;
using ProjectXyz.Application.Core.Actors;
using ProjectXyz.Application.Core.Interactions;
using ProjectXyz.Application.Interface.Actors;
using ProjectXyz.Application.Interface.Interactions;
using UnityEngine;

namespace Assets.Scripts.Actors.Player
{
    public class PlayerBehaviour : ActorBehaviour, IPlayerBehaviour
    {
        #region Fields
        private readonly Dictionary<IInteractable, GameObject> _interactables;
        #endregion

        #region Constructors
        public PlayerBehaviour()
        {
            _interactables = new Dictionary<IInteractable, GameObject>();
        }
        #endregion

        #region Properties
        public IActor Player
        {
            get { return Actor; }
        }

        public IEnumerable<IInteractable> Interactables
        {
            get
            {
                var interactables = _interactables.Keys.ToArray();
                foreach (var interactable in interactables)
                {
                    if (_interactables[interactable] == null)
                    {
                        Debug.Log("Pruning " + interactable);
                        _interactables.Remove(interactable);
                        continue;
                    }

                    yield return interactable;
                }
            }
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

        public void OnTriggerEnter2D(Collider2D collider)
        {
            var interactableBehaviour = (IInteractableBehaviour)collider.gameObject.GetComponent(typeof(IInteractableBehaviour));
            if (interactableBehaviour == null)
            {
                return;
            }

            _interactables.Add(interactableBehaviour.Interactable, collider.gameObject);
        }

        public void OnTriggerExit2D(Collider2D collider)
        {
            var interactableBehaviour = (IInteractableBehaviour)collider.gameObject.GetComponent(typeof(IInteractableBehaviour));
            if (interactableBehaviour == null)
            {
                return;
            }

            _interactables.Remove(interactableBehaviour.Interactable);
        }
        #endregion
    }
}
