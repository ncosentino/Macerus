using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Api.GameObjects;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Logging;

namespace Macerus.Game.GameObjects
{
    public sealed class GameObjectRepositoryFacade : IGameObjectRepositoryFacade
    {
        private readonly List<Tuple<CanLoadGameObjectDelegate, LoadGameObjectDelegate>> _loadMapping;
        private readonly List<Tuple<CanCreateFromTemplateDelegate, CreateObjectFromTemplateDelegate>> _createFromTemplateMapping;
        private readonly ILogger _logger;

        public GameObjectRepositoryFacade(
            IEnumerable<IDiscoverableGameObjectRepository> discoverableGameObjectRepositories,
            ILogger logger)
        {
            _loadMapping = new List<Tuple<CanLoadGameObjectDelegate, LoadGameObjectDelegate>>();
            _createFromTemplateMapping = new List<Tuple<CanCreateFromTemplateDelegate, CreateObjectFromTemplateDelegate>>();
            _logger = logger;

            foreach (var repository in discoverableGameObjectRepositories)
            {
                RegisterRepository(
                    repository.CanLoad,
                    repository.Load);
                RegisterRepository(
                    repository.CanCreateFromTemplate,
                    repository.CreateFromTemplate);
            }
        }

        public IGameObject Load(
            IIdentifier typeId,
            IIdentifier objectId)
        {
            var repository = _loadMapping.FirstOrDefault(x => x.Item1(
                typeId,
                objectId));
            if (repository == null)
            {
                throw new InvalidOperationException(
                    $"There is no repository that can load '{objectId}' for " +
                    $"type '{typeId}'.");
            }

            _logger.Debug(
                $"Using repository '{repository}' to load object of type " +
                $"'{typeId}' with id '{objectId}'.");
            return repository.Item2(typeId, objectId);
        }

        public IGameObject CreateFromTemplate(
            IIdentifier typeId,
            IIdentifier templateId,
            IReadOnlyDictionary<string, object> properties)
        {
            var repository = _createFromTemplateMapping.FirstOrDefault(x => x.Item1(
                typeId,
                templateId));
            if (repository == null)
            {
                throw new InvalidOperationException(
                    $"There is no repository that can create object from " +
                    $"template '{templateId}' for type '{typeId}'.");
            }

            _logger.Debug(
                $"Using repository '{repository}' to create object of type " +
                $"'{typeId}' with template '{templateId}'.");
            return repository.Item2(
                typeId,
                templateId,
                properties);
        }

        public void RegisterRepository(
             CanCreateFromTemplateDelegate canCreateFromTemplateCallback,
             CreateObjectFromTemplateDelegate createObjectFromTemplateCallback)
        {
            _createFromTemplateMapping.Add(new Tuple<CanCreateFromTemplateDelegate, CreateObjectFromTemplateDelegate>(
                canCreateFromTemplateCallback,
                createObjectFromTemplateCallback));
        }

        public void RegisterRepository(
            CanLoadGameObjectDelegate canLoadCallback,
            LoadGameObjectDelegate loadCallback)
        {
            _loadMapping.Add(new Tuple<CanLoadGameObjectDelegate, LoadGameObjectDelegate>(
                canLoadCallback,
                loadCallback));
        }
    }
}
