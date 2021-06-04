using Autofac;

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
        }
    }
}
