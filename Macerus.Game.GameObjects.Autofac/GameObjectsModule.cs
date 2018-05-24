using Autofac;

namespace Macerus.Game.GameObjects.Autofac
{
    public class GameObjectsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<GameObjectRepositoryFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
