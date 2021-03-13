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
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.GameObjects.Actors.Npc
{
    public sealed class NpcTemplateRepository : IDiscoverableActorTemplateRepository
    {
        private readonly IGameObjectIdentifiers _gameObjectIdentifiers;
        private readonly IActorGeneratorFacade _actorGeneratorFacade;
        private readonly IBehaviorManager _behaviorManager;
        private readonly IFilterContextAmenity _filterContextAmenity;

        public NpcTemplateRepository(
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
                new FilterAttribute(
                    _gameObjectIdentifiers.FilterContextTemplateId,
                    new NotFilterAttributeValue(new IdentifierFilterAttributeValue(new StringIdentifier("player"))),
                    true),
            };
        }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; }

        public IEnumerable<IGameObject> CreateFromTemplate(
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

            // FIXME: is the API for this wrong now? should we ever be
            // returning more than one thing here, especially if the use case
            // is actually for populating things on a map?
            foreach (var generatedActor in generatedActors)
            {
                var npc = new Npc(
                    new IBehavior[]
                    {
                        new TemplateIdentifierBehavior()
                        {
                            TemplateId = _filterContextAmenity.GetGameObjectTemplateIdFromContext(filterContext),
                        },
                    }.Concat(generatedActor.Behaviors));
                _behaviorManager.Register(npc, npc.Behaviors);

                var worldLocation = npc.Get<IWorldLocationBehavior>().Single();
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

                yield return npc;
            }
        }
    }
}
