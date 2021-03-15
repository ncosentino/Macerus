using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using Macerus.Api.Behaviors;
using Macerus.Api.Behaviors.Filtering;
using Macerus.Api.GameObjects;
using Macerus.Plugins.Features.GameObjects.Actors.Generation;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.GameObjects.Actors.Player
{
    public sealed class PlayerTemplateRepository : IDiscoverableActorTemplateRepository
    {
        private readonly IGameObjectIdentifiers _gameObjectIdentifiers;
        private readonly IActorGeneratorFacade _actorGeneratorFacade;
        private readonly IBehaviorManager _behaviorManager;
        private readonly IFilterContextAmenity _filterContextAmenity;

        public PlayerTemplateRepository(
            IBehaviorManager behaviorManager,
            IFilterContextAmenity filterContextAmenity,
            IGameObjectIdentifiers gameObjectIdentifiers,
            IActorGeneratorFacade actorGeneratorFacade)
        {
            _behaviorManager = behaviorManager;
            _filterContextAmenity = filterContextAmenity;
            _gameObjectIdentifiers = gameObjectIdentifiers;
            _actorGeneratorFacade = actorGeneratorFacade;

            SupportedAttributes = new[]
            {
                _filterContextAmenity.CreateSupportedAttribute(
                    _gameObjectIdentifiers.FilterContextTemplateId,
                    new StringIdentifier("player")),
            };
        }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; }

        public IGameObject CreateFromTemplate(
            IFilterContext filterContext,
            IReadOnlyDictionary<string, object> properties)
        {
            var subContext = _filterContextAmenity
                .CreateSubContext(
                    filterContext,
                    SupportedAttributes);
            var generatedActors = _actorGeneratorFacade
                .GenerateActors(subContext)
                .ToArray();
            if (generatedActors.Length != 1)
            {
                throw new InvalidOperationException(
                    $"Expecting only one actor to be generated when creating " +
                    $"a player but {generatedActors.Length} were generated.");
            }

            var generatedActor = generatedActors.Single();

            var player = new Player(
                new IBehavior[]
                {
                    new TemplateIdentifierBehavior()
                    {
                        TemplateId = _filterContextAmenity.GetGameObjectTemplateIdFromContext(filterContext),
                    },
                }.Concat(generatedActor.Behaviors));
            _behaviorManager.Register(player, player.Behaviors);

            var worldLocation = player.Get<IWorldLocationBehavior>().Single();
            worldLocation.X = properties.TryGetValue("X", out var rawX)
                ? Convert.ToDouble(rawX, CultureInfo.InvariantCulture)
                : 0;
            worldLocation.Y = properties.TryGetValue("Y", out var rawY)
                ? Convert.ToDouble(rawY, CultureInfo.InvariantCulture)
                : 0;
            worldLocation.Width = properties.TryGetValue("Width", out var rawWidth)
                ? Convert.ToDouble(rawWidth, CultureInfo.InvariantCulture)
                : 1;
            worldLocation.Height = properties.TryGetValue("Height", out var rawHeight)
                ? Convert.ToDouble(rawHeight, CultureInfo.InvariantCulture)
                : 1;

            return player;
        }
    }
}
