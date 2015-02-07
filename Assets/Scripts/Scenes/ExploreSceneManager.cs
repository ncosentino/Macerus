using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Maps;
using Mono.Data.SqliteClient;
using ProjectXyz.Application.Core;
using ProjectXyz.Application.Core.Maps;
using ProjectXyz.Application.Interface;
using ProjectXyz.Data.Sql;
using UnityEngine;

namespace Assets.Scripts.Scenes
{
    public sealed class ExploreSceneManager : SingletonBehaviour<ExploreSceneManager>, IExploreSceneManager
    {
        #region Fields
        private readonly IManager _manager;

        private GameObject _player;
        private GameObject _map;
        private IMapLoader _mapLoader;
        #endregion

        #region Constructors
        /// <summary>
        /// Prevents a default instance of the <see cref="ExploreSceneManager"/> class from being created.
        /// </summary>
        private ExploreSceneManager()
        {
            var connectionString = "URI=file::memory:,version=3";
            var connection = new SqliteConnection(connectionString);
            connection.Open();
            
            var database = SqlDatabase.Create(connection, true);
            var upgrader = SqlDatabaseUpgrader.Create();
            var dataStore = SqlDataStore.Create(database, upgrader);

            _manager = Manager.Create(dataStore);
        }
        #endregion

        #region Properties
        public GameObject Player
        {
            get
            {
                if (_player == null)
                {
                    _player = GameObject.Find("Player");     
                }
                
                return _player;
            }
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
            _mapLoader.LoadMap(loadMapProperties);
            _player = null;
        }
        #endregion
    }

    public interface IExploreSceneManager : IMapLoader
    {
        #region Properties
        GameObject Player { get; }
        #endregion
    }
}
