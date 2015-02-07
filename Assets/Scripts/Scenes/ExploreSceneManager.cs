﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Assets.Scripts.Maps;
using UnityEngine;

namespace Assets.Scripts.Scenes
{
    public sealed class ExploreSceneManager : SingletonBehaviour<ExploreSceneManager>, IExploreSceneManager
    {
        #region Fields
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
            _mapLoader.LoadMap(new LoadMapProperties("Assets/Resources/Maps/swamp.tmx"));
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
