using System.Collections.Generic;
using Assets.Scripts.Encounters;
using Assets.Scripts.Interactables;
using Assets.Scripts.Interactables.Teleporters;
using ProjectXyz.Application.Interface.Actors;

namespace Assets.Scripts.Actors.Player
{
    public interface IPlayerBehaviour : IActorBehaviour, ICanTeleport, ICanEncounter, ICanInteract
    {
        #region Properties
        IActor Player { get; }
        #endregion
    }
}
