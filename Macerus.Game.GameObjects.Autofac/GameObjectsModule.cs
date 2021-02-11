using System.Collections.Generic;

using Autofac;

using Macerus.Api.GameObjects;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Game.GameObjects.Autofac
{
    public class GameObjectsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<GameObjectRepositoryFacade>()
                .AsImplementedInterfaces()
                .SingleInstance()
                .OnActivated(x =>
                {
                    var facade = x.Instance;
                    var discoverableRepositories = x
                        .Context
                        .Resolve<IEnumerable<IDiscoverableGameObjectRepository>>();
                    foreach (var repository in discoverableRepositories)
                    {
                        facade.RegisterRepository(
                            repository.CanLoad,
                            repository.Load);
                    }
                });
        }
    }
}
