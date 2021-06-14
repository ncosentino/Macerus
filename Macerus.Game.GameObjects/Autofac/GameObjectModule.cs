using System.Collections.Generic;

using Autofac;

using Macerus.Game.Api.Scenes;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Game.Autofac
{
    public class GameObjectModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
               .RegisterType<GameObjectRepositoryAmenity>()
               .AsImplementedInterfaces()
               .SingleInstance();
            builder
                .RegisterType<GameObjectRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<CombatEndTemplateSpawnerSystem>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<UpdateFrequencySystem>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<UpdateFrequencyManager>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterBuildCallback(c =>
                {
                    var logger = c.Resolve<ProjectXyz.Api.Logging.ILogger>();
                    foreach (var sceneLoadHook in c.Resolve<IEnumerable<IDiscoverableSceneLoadHook>>())
                    {
                        logger.Debug($"Created scene load hook '{sceneLoadHook}'.");
                    }
                });
        }
    }
}
