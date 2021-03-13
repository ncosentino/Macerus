using System.Collections.Generic;
using System.Linq;

using Macerus.Api.Behaviors.Filtering;
using Macerus.Api.GameObjects;

using NexusLabs.Contracts;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

namespace Macerus.Game.GameObjects
{
    public sealed class GameObjectRepositoryAmenity : IGameObjectRepositoryAmenity
    {
        private readonly IGameObjectRepositoryFacade _gameObjectRepositoryFacade;
        private readonly IFilterContextAmenity _filterContextAmenity;
        private readonly IFilterContextFactory _filterContextFactory;

        public GameObjectRepositoryAmenity(
            IGameObjectRepositoryFacade gameObjectRepositoryFacade,
            IFilterContextAmenity filterContextAmenity,
            IFilterContextFactory filterContextFactory)
        {
            _gameObjectRepositoryFacade = gameObjectRepositoryFacade;
            _filterContextAmenity = filterContextAmenity;
            _filterContextFactory = filterContextFactory;
        }

        public IGameObject LoadSingleGameObject(IIdentifier typeId, IIdentifier id)
        {
            var filterContext = _filterContextFactory.CreateFilterContextForSingle();
            filterContext = _filterContextAmenity.ExtendWithGameObjectTypeIdFilter(
                filterContext,
                typeId);
            filterContext = _filterContextAmenity.ExtendWithGameObjectIdFilter(
                 filterContext,
                 id);
            var gameObjects = _gameObjectRepositoryFacade
                .Load(filterContext)
                .ToArray();
            Contract.Requires(
                gameObjects.Length == 1,
                $"Expecting exactly 1 game object to match type ID '{typeId}' " +
                $"and ID '{id}' but there were {gameObjects.Length}.");
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
            var gameObjects = _gameObjectRepositoryFacade
                .CreateFromTemplate(
                    filterContext,
                    properties)
                .ToArray();
            Contract.Requires(
                gameObjects.Length == 1,
                $"Expecting exactly 1 game object to match type ID '{typeId}' " +
                $"and template ID '{templateId}' but there were {gameObjects.Length}.");
            var gameObject = gameObjects[0];
            return gameObject;
        }
    }
}
