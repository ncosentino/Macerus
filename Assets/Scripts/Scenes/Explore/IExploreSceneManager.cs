using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Actors.Player;
using ProjectXyz.Application.Interface;
using ProjectXyz.Application.Interface.Actors;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Maps;
using ProjectXyz.Game.Interface;

namespace Assets.Scripts.Scenes.Explore
{
    public interface IExploreSceneManager
    {
        #region Properties
        IPlayerBehaviourRegistrar PlayerBehaviourRegistrar { get; }

        IGameManager Manager { get; }

        IActorContext ActorContext { get; }

        IItemContext ItemContext { get; }

        IEnchantmentContext EnchantmentContext { get; }
        #endregion

        #region Methods
        void LoadMap(Guid mapId);
        #endregion
    }
}
