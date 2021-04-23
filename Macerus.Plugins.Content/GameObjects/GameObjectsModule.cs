using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Content.GameObjects
{
    public class GameObjectsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<GameObjectIdentifiers>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
