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
        private readonly List<Tuple<CanLoadGameObjectDelegate, LoadGameObjectDelegate>> _repositoryMapping;
        private readonly ILogger _logger;

        public GameObjectRepositoryFacade(ILogger logger)
        {
            _repositoryMapping = new List<Tuple<CanLoadGameObjectDelegate, LoadGameObjectDelegate>>();
            _logger = logger;
        }

        public IGameObject Load(IIdentifier typeId, IIdentifier objectId)
        {
            var repository = _repositoryMapping.FirstOrDefault(x => x.Item1(
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

        public void RegisterRepository(
            CanLoadGameObjectDelegate canLoadCallback,
            LoadGameObjectDelegate loadCallback)
        {
            _repositoryMapping.Add(new Tuple<CanLoadGameObjectDelegate, LoadGameObjectDelegate>(
                canLoadCallback,
                loadCallback));
        }
    }
}
