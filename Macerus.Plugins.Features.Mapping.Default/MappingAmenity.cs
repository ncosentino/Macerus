﻿using System.Collections.Generic;
using System.Numerics;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.GameObjects.Actors;
using Macerus.Plugins.Features.Stats;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Mapping;

namespace Macerus.Plugins.Features.Mapping.Default
{
    public sealed class MappingAmenity : IMappingAmenity
    {
        private readonly IMapProvider _mapProvider;
        private readonly IMapGameObjectManager _mapGameObjectManager;
        private readonly IMacerusActorIdentifiers _actorIdentifiers;
        private readonly IStatCalculationServiceAmenity _statCalculationServiceAmenity;

        public MappingAmenity(
            IMapProvider mapProvider,
            IMapGameObjectManager mapGameObjectManager,
            IMacerusActorIdentifiers actorIdentifiers,
            IStatCalculationServiceAmenity statCalculationServiceAmenity)
        {
            _mapProvider = mapProvider;
            _mapGameObjectManager = mapGameObjectManager;
            _actorIdentifiers = actorIdentifiers;
            _statCalculationServiceAmenity = statCalculationServiceAmenity;
        }

        public IPathFinder CurrentPathFinder =>
            _mapProvider.PathFinder;

        public IReadOnlyCollection<IGameObject> GameObjects =>
            _mapGameObjectManager.GameObjects;

        public void MarkForAddition(IGameObject obj) =>
            _mapGameObjectManager.MarkForAddition(obj);

        public void MarkForRemoval(IGameObject obj) =>
            _mapGameObjectManager.MarkForRemoval(obj);

        public IEnumerable<Vector2> GetAllowedPathDestinationsForActor(IGameObject actor)
        {
            var movementBehavior = actor.GetOnly<IMovementBehavior>();
            var positionBehavior = actor.GetOnly<IReadOnlyPositionBehavior>();
            var sizeBehavior = actor.GetOnly<IReadOnlySizeBehavior>();
            var allowedWalkDiagonally = _statCalculationServiceAmenity.GetStatValue(
                movementBehavior.Owner,
                _actorIdentifiers.MoveDiagonallyStatDefinitionId) > 0;
            var allowedWalkDistance = _statCalculationServiceAmenity.GetStatValue(
                movementBehavior.Owner,
                _actorIdentifiers.MoveDistancePerTurnCurrentStatDefinitionId);
            var actorPosition = new Vector2(
                (float)positionBehavior.X,
                (float)positionBehavior.Y);
            var actorSize = new Vector2(
                (float)sizeBehavior.Width,
                (float)sizeBehavior.Height);

            var validWalkPoints = _mapProvider
                .PathFinder
                .GetAllowedPathDestinations(
                    actorPosition,
                    actorSize,
                    allowedWalkDistance,
                    allowedWalkDiagonally);
            return validWalkPoints;
        }
    }
}
