using System.Collections.Generic;
using System.Linq;

using Macerus.Api.Behaviors.Filtering;
using Macerus.Api.GameObjects;

using NexusLabs.Contracts;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Game.Api;

namespace Macerus.Game.GameObjects
{
    public sealed class GameObjectRepositoryAmenity : IGameObjectRepositoryAmenity
    {
        private readonly IGameObjectTemplateRepositoryFacade _gameObjectTemplateRepositoryFacade;
        private readonly IGameObjectRepository _gameObjectRepository;
        private readonly IFilterContextAmenity _filterContextAmenity;
        private readonly IFilterContextFactory _filterContextFactory;

        public GameObjectRepositoryAmenity(
            IGameObjectTemplateRepositoryFacade gameObjectTemplateRepositoryFacade,
            IGameObjectRepository gameObjectRepository,
            IFilterContextAmenity filterContextAmenity,
            IFilterContextFactory filterContextFactory)
        {
            _gameObjectTemplateRepositoryFacade = gameObjectTemplateRepositoryFacade;
            _gameObjectRepository = gameObjectRepository;
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
            IIdentifier typeId,
            IIdentifier templateId,
            IReadOnlyDictionary<string, object> properties)
        {
            var filterContext = _filterContextFactory.CreateFilterContextForSingle();
            filterContext = _filterContextAmenity.ExtendWithGameObjectTypeIdFilter(
                filterContext,
                typeId);
            filterContext = _filterContextAmenity.ExtendWithGameObjectTemplateIdFilter(
                filterContext,
                templateId);
            var gameObject = _gameObjectTemplateRepositoryFacade.CreateFromTemplate(
                filterContext,
                properties);
            return gameObject;
        }
    }
}
