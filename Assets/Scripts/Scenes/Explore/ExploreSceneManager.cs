using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Actors;
using Assets.Scripts.Actors.Player;
using Assets.Scripts.GameObjects;
using Assets.Scripts.Maps;
using Assets.Scripts.Scenes.Explore.Gui;
using Assets.Scripts.Scenes.Explore.Input;
using Mono.Data.Sqlite;
using ProjectXyz.Application.Core.Maps;
using ProjectXyz.Application.Interface.Actors;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Maps;
using ProjectXyz.Application.Interface.Time;
using ProjectXyz.Data.Sql;
using ProjectXyz.Game.Core;
using ProjectXyz.Game.Interface;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore
{
    public sealed class ExploreSceneManager : SingletonBehaviour<ExploreSceneManager, IExploreSceneManager>, IExploreSceneManager
    {
        #region Fields
        private readonly IGameManager _manager;
        private readonly IEnchantmentContext _enchantmentContext;
        private readonly IItemContext _itemContext;
        private readonly IActorContext _actorContext;
        private readonly IPlayerBehaviourRegistrar _playerBehaviourRegistrar;
        private readonly IKeyboardControls _keyboardControls;

        private IMutableCalendar _calendar;

        private GameObject _map;
        private IMapLoader _mapLoader;
        private IMapContext _mapContext;

        private IHudController _hudController;
        private IPlayerInputController _playerInputController;
        private IGuiInputController _guiInputController;
        #endregion

        #region Unity Properties
        public Transform CanvasTransform;

        public Transform InventoryWindowTransform;
        #endregion

        #region Constructors
        /// <summary>
        /// Prevents a default instance of the <see cref="ExploreSceneManager"/> class from being created.
        /// </summary>
        private ExploreSceneManager()
        {
            _keyboardControls = new KeyboardControls();

            _playerBehaviourRegistrar = new PlayerBehaviourRegistrar();
            _playerBehaviourRegistrar.PlayerRegistered += PlayerBehaviourRegistrar_PlayerRegistered;
            _playerBehaviourRegistrar.PlayerUnregistered += PlayerBehaviourRegistrar_PlayerUnregistered;

            var connectionString = "URI=file::memory:,version=3";
            var connection = new SqliteConnection(connectionString);
            connection.Open();
            
            var database = SqlDatabase.Create(connection, true);
            var upgrader = SqlDatabaseUpgrader.Create();
            var dataManager = SqlDataManager.Create(
                database,
                upgrader);
            
            _manager = GameManager.Create(
                database,
                dataManager,
                Enumerable.Empty<string>());

            // FIXME: initializing these contexts here seems like a shit sandwich
            _enchantmentContext = ProjectXyz.Application.Core.Enchantments.EnchantmentContext.Create();
            ////_itemContext = ProjectXyz.Application.Core.Items.ItemContext.Create(
            ////    enchantmentCalculator,
            ////    _manager.ApplicationManager.Enchantments.Factories.Enchantments,
            ////    dataManager.Items.StatSocketTypes);
            _actorContext = ProjectXyz.Application.Core.Actors.ActorContext.Create(_manager.ApplicationManager.Enchantments.EnchantmentCalculator);
        }
        #endregion

        #region Properties
        public IPlayerBehaviourRegistrar PlayerBehaviourRegistrar
        {
            get { return _playerBehaviourRegistrar; }
        }

        public IGameManager Manager
        {
            get { return _manager; }
        }

        public IActorContext ActorContext
        {
            get { return _actorContext; }
        }

        public IItemContext ItemContext
        {
            get { return _itemContext; }
        }

        public IEnchantmentContext EnchantmentContext
        {
            get { return _enchantmentContext; }
        }
        #endregion

        #region Methods
        public void Start()
        {
            var initialDateTime = ProjectXyz.Application.Core.Time.DateTime.Create(0, 0, 0, 0, 0, 0);
            _calendar = ProjectXyz.Application.Core.Time.Calendar.Create(initialDateTime);

            _mapContext = MapContext.Create(_calendar);

            _map = new GameObject();
            _map.name = "Map";
            _map.transform.parent = this.transform;

            _mapLoader = _map.AddComponent<MapBehaviour>();
            LoadMap(Guid.NewGuid());

            _hudController = new HudController(CanvasTransform, InventoryWindowTransform);
            _hudController.ShowInventory(false);

            _guiInputController = new GuiInputController(_keyboardControls, _hudController);
        }

        public void Update()
        {
            _guiInputController.Update(Time.deltaTime);

            if (_playerInputController != null)
            {
                _playerInputController.Update(Time.deltaTime);
            }

            _calendar.UpdateElapsedTime(TimeSpan.FromSeconds(Time.deltaTime));
        }

        public void LoadMap(Guid mapId)
        {            
            _playerBehaviourRegistrar.UnregisterPlayer(_playerBehaviourRegistrar.PlayerBehaviour);

            var mapLoadProperties = new LoadMapProperties(
                _manager,
                _mapContext,
                mapId);
            _mapLoader.LoadMap(mapLoadProperties);
        }

        private void OnDestroy()
        {
            _playerBehaviourRegistrar.PlayerRegistered -= PlayerBehaviourRegistrar_PlayerRegistered;
            _playerBehaviourRegistrar.PlayerUnregistered -= PlayerBehaviourRegistrar_PlayerUnregistered;
        }
        #endregion

        #region Event Handlers
        private void PlayerBehaviourRegistrar_PlayerUnregistered(object sender, PlayerBehaviourRegisteredEventArgs e)
        {
            Debug.Log("Player unregistered. Destroying movement controls.");
            _playerInputController = null;
        }

        private void PlayerBehaviourRegistrar_PlayerRegistered(object sender, PlayerBehaviourRegisteredEventArgs e)
        {
            var actorMovementBehaviour = e.PlayerBehaviour.ActorGameObject.GetRequiredComponent<IActorMovementBehaviour>();
            
            Debug.Log("Making movement controls.");
            _playerInputController = new PlayerInputController(
                _keyboardControls,
                actorMovementBehaviour,
                e.PlayerBehaviour);

            // TODO: remove this test code.
            Debug.Log("Setting some test state.");
            e.PlayerBehaviour.Player.Inventory.ItemCapacity = 100;
        }
        #endregion
    }    
}
