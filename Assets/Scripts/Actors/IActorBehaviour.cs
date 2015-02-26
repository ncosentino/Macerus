using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Encounters;
using ProjectXyz.Application.Interface.Actors;
using UnityEngine;

namespace Assets.Scripts.Actors
{
    public interface IActorBehaviour
    {
        #region Properties
        GameObject ActorGameObject { get; }

        IActor Actor { get; }
        #endregion
    }
}
