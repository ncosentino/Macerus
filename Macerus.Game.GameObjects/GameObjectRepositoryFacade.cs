using System;
using System.Collections.Generic;
using System.Linq;
using Macerus.Api.GameObjects;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

namespace Macerus.Game.GameObjects
{
    public sealed class GameObjectRepositoryFacade : IGameObjectRepositoryFacade
    {
        private readonly List<Tuple<CanLoadGameObjectDelegate, LoadGameObjectDelegate>> _repositoryMapping;

        public GameObjectRepositoryFacade()
        {
            _repositoryMapping = new List<Tuple<CanLoadGameObjectDelegate, LoadGameObjectDelegate>>();
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

            return repository.Item2(objectId);
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
