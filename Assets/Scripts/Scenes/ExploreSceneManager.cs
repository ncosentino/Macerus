using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Actors.Player;
using Assets.Scripts.Maps;
using Mono.Data.SqliteClient;
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Core.Maps;
using ProjectXyz.Application.Interface;
using ProjectXyz.Application.Interface.Actors;
using ProjectXyz.Data.Sql;
using UnityEngine;

namespace Assets.Scripts.Scenes
{
    public sealed class ExploreSceneManager : SingletonBehaviour<ExploreSceneManager, IExploreSceneManager>, IExploreSceneManager
    {
        #region Fields
        private readonly IManager _manager;
        private readonly IActorContext _actorContext;
        private readonly IPlayerBehaviourRegistrar _playerBehaviourRegistrar;

        private IPlayerBehaviour _player;
        private GameObject _map;
        private IMapLoader _mapLoader;
        #endregion

        #region Constructors
        /// <summary>
        /// Prevents a default instance of the <see cref="ExploreSceneManager"/> class from being created.
        /// </summary>
        private ExploreSceneManager()
        {
            _playerBehaviourRegistrar = new PlayerBehaviourRegistrar();

            var connectionString = "URI=file::memory:,version=3";
            var connection = new SqliteConnection(connectionString);
            connection.Open();
            
            var database = SqlDatabase.Create(connection, true);
            var upgrader = SqlDatabaseUpgrader.Create();
            var dataStore = SqlDataStore.Create(database, upgrader);

            _manager = ProjectXyz.Application.Core.Manager.Create(dataStore);

            var enchantmentCalculator = EnchantmentCalculator.Create();
            _actorContext = ProjectXyz.Application.Core.Actors.ActorContext.Create(enchantmentCalculator);
        }
        #endregion

        #region Properties
        public IPlayerBehaviourRegistrar PlayerBehaviourRegistrar
        {
            get { return _playerBehaviourRegistrar; }
        }

        public IManager Manager
        {
            get { return _manager; }
        }

        public IActorContext ActorContext
        {
            get { return _actorContext; }
        }
        #endregion

        #region Methods
        public void Start()
        {
            _map = new GameObject();
            _map.name = "Map";
            _map.transform.parent = this.transform;

            _mapLoader = _map.AddComponent<MapBehaviour>();
            
            var mapContext = MapContext.Create();
            var mapAssetPath = _manager.Maps.GetMapById(Guid.NewGuid(), mapContext).ResourceName;
            var mapLoadProperties = new LoadMapProperties(mapAssetPath);
            _mapLoader.LoadMap(mapLoadProperties);
        }

        public void LoadMap(ILoadMapProperties loadMapProperties)
        {
            _playerBehaviourRegistrar.UnregisterPlayer(_player);
            _mapLoader.LoadMap(loadMapProperties);
        }
        #endregion
    }    
}
