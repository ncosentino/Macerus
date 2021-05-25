using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Api.Behaviors.Filtering;
using Macerus.Api.GameObjects;
using Macerus.Shared.Behaviors;

using NexusLabs.Contracts;

using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Game.Api;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Framework;

namespace Macerus.Game
{
    public sealed class GameObjectRepositoryAmenity : IGameObjectRepositoryAmenity
    {
        private readonly IGameObjectTemplateRepository _gameObjectTemplateRepository;
        private readonly IGameObjectRepository _gameObjectRepository;
        private readonly IGameObjectFactory _gameObjectFactory;
        private readonly IFilterContextAmenity _filterContextAmenity;
        private readonly IFilterContextFactory _filterContextFactory;

        public GameObjectRepositoryAmenity(
            IGameObjectTemplateRepository gameObjectTemplateRepository,
            IGameObjectRepository gameObjectRepository,
            IGameObjectFactory gameObjectFactory,
            IFilterContextAmenity filterContextAmenity,
            IFilterContextFactory filterContextFactory)
        {
            _gameObjectTemplateRepository = gameObjectTemplateRepository;
            _gameObjectRepository = gameObjectRepository;
            _gameObjectFactory = gameObjectFactory;
            _filterContextAmenity = filterContextAmenity;
            _filterContextFactory = filterContextFactory;
        }

        public void SaveGameObject(IGameObject gameObject)
        {
            _gameObjectRepository.Save(gameObject);
        }

        public IGameObject LoadGameObject(IIdentifier id)
        {
            var filterContext = _filterContextFactory.CreateFilterContextForSingle();
            filterContext = _filterContextAmenity.ExtendWithGameObjectIdFilter(
                 filterContext,
                 id);
            var gameObjects = _gameObjectRepository
                .Load(filterContext)
                .ToArray();
            Contract.Requires(
                gameObjects.Length == 1,
                $"Expecting exactly 1 game object to match ID '{id}' but there " +
                $"were {gameObjects.Length}.");
            var gameObject = gameObjects[0];
            return gameObject;
        }

        public IGameObject CreateGameObjectFromTemplate(
            IIdentifier templateId,
            IEnumerable<IBehavior> additionalBehaviors)
        {
            var filterContext = _filterContextFactory.CreateFilterContextForSingle();
            filterContext = _filterContextAmenity.ExtendWithGameObjectTemplateIdFilter(
                filterContext,
                templateId);
            var templates = _gameObjectTemplateRepository
                .GetTemplates(filterContext)
                .ToArray();
            Contract.Requires(
                templates.Length == 1,
                $"Expecting exactly 1 template to match ID '{templateId}' but there " +
                $"were {templates.Length}.");
            var template = templates.Single();

            // FIXME: do we need to handle merging behaviors and other fun stuff?
            var newBehaviorTypes = new HashSet<Type>(additionalBehaviors.Select(x => x.GetType()));
            var newBehaviors = template
                .Behaviors
                .Where(x => !newBehaviorTypes.Contains(x.GetType()))
                .Concat(additionalBehaviors)
                .ToList();

            // FIXME: we should make the editor enforce we always have an identifier
            if (!newBehaviors.TakeTypes<IReadOnlyIdentifierBehavior>().Any())
            {
                newBehaviors.Add(new IdentifierBehavior(new StringIdentifier(Guid.NewGuid().ToString())));
            }

            newBehaviors.Add(new CreatedFromTemplateBehavior(templateId));
            
            var newGameObject = _gameObjectFactory.Create(newBehaviors);
            return newGameObject;
        }
    }
}
