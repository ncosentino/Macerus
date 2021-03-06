﻿using System;
using System.Linq;
using System.Threading.Tasks;

using Macerus.Api.Behaviors.Filtering;
using Macerus.Plugins.Features.GameObjects.Actors;
using Macerus.Plugins.Features.GameObjects.Actors.Generation;
using Macerus.Plugins.Features.PartyBar;
using Macerus.Plugins.Features.StatusBar.Api;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Game.Api;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Mapping;
using ProjectXyz.Plugins.Features.PartyManagement;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.MainMenu.Default.NewGame
{
    public sealed class NewGameWorkflow : INewGameWorkflow
    {
        private readonly Lazy<IMapStateRepository> _lazyMapStateRepository;
        private readonly Lazy<IGameObjectRepository> _lazyGameObjectRepository;
        private readonly Lazy<IFilterContextAmenity> _lazyFilterContextAmenity;
        private readonly Lazy<IGameObjectIdentifiers> _lazyGameObjectIdentifiers;
        private readonly Lazy<IMacerusActorIdentifiers> _lazyMacerusActorIdentifiers;
        private readonly Lazy<IActorGeneratorFacade> _lazyActorGeneratorFacade;
        private readonly Lazy<IMapManager> _lazyMapManager;
        private readonly Lazy<IRosterManager> _lazyRosterManager;
        private readonly Lazy<IStatusBarViewModel> _lazyStatusBarViewModel;
        private readonly Lazy<IPartyBarController> _lazyPartyBarController;

        public NewGameWorkflow(
            Lazy<IMapStateRepository> lazyMapStateRepository,
            Lazy<IGameObjectRepository> lazyGameObjectRepository,
            Lazy<IFilterContextAmenity> lazyFilterContextAmenity,
            Lazy<IGameObjectIdentifiers> lazyGameObjectIdentifiers,
            Lazy<IMacerusActorIdentifiers> lazyMacerusActorIdentifiers,
            Lazy<IActorGeneratorFacade> lazyActorGeneratorFacade,
            Lazy<IMapManager> lazyMapManager,
            Lazy<IRosterManager> lazyRosterManager,
            Lazy<IStatusBarViewModel> lazyStatusBarViewModel,
            Lazy<IPartyBarController> lazyPartyBarController)
        {
            _lazyMapStateRepository = lazyMapStateRepository;
            _lazyGameObjectRepository = lazyGameObjectRepository;
            _lazyFilterContextAmenity = lazyFilterContextAmenity;
            _lazyGameObjectIdentifiers = lazyGameObjectIdentifiers;
            _lazyMacerusActorIdentifiers = lazyMacerusActorIdentifiers;
            _lazyActorGeneratorFacade = lazyActorGeneratorFacade;
            _lazyMapManager = lazyMapManager;
            _lazyRosterManager = lazyRosterManager;
            _lazyStatusBarViewModel = lazyStatusBarViewModel;
            _lazyPartyBarController = lazyPartyBarController;
        }

        public async Task RunAsync()
        {
            _lazyGameObjectRepository.Value.Clear();
            _lazyMapStateRepository.Value.ClearState();
            _lazyRosterManager.Value.ClearRoster();

            var player = CreateActor(new StringIdentifier("player"));
            player.GetOnly<IPositionBehavior>().SetPosition(40, -16);
            _lazyGameObjectRepository.Value.Save(player);
            _lazyRosterManager.Value.AddToRoster(player);
            player.GetOnly<IRosterBehavior>().IsPartyLeader = true;

            var mercenary = CreateActor(new StringIdentifier("test-mercenary"));
            _lazyGameObjectRepository.Value.Save(mercenary);
            _lazyRosterManager.Value.AddToRoster(mercenary);
            mercenary.GetOnly<IRosterBehavior>().IsActiveParty = true;

            _lazyStatusBarViewModel.Value.IsOpen = true;
            _lazyPartyBarController.Value.ShowPartyBar();

            await _lazyMapManager
                .Value
                .SwitchMapAsync(new StringIdentifier("swamp"))
                .ConfigureAwait(false);
        }

        private IGameObject CreateActor(IIdentifier definitionId)
        {
            var filterContextAmenity = _lazyFilterContextAmenity.Value;
            var actorIdentifiers = _lazyMacerusActorIdentifiers.Value;
            var gameObjectIdentifiers = _lazyGameObjectIdentifiers.Value;
            var actorGeneratorFacade = _lazyActorGeneratorFacade.Value;
            var context = filterContextAmenity.CreateFilterContextForSingle(
                filterContextAmenity.CreateRequiredAttribute(
                    gameObjectIdentifiers.FilterContextTypeId,
                    actorIdentifiers.ActorTypeIdentifier),
                filterContextAmenity.CreateRequiredAttribute(
                    actorIdentifiers.ActorDefinitionIdentifier,
                    definitionId));
            var player = actorGeneratorFacade
                .GenerateActors(
                    context,
                    new IGeneratorComponent[] { })
                .Single();
            return player;
        }
    }
}
