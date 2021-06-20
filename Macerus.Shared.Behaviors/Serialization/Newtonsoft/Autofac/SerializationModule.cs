using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Shared.Behaviors.Serialization.Newtonsoft.Autofac
{
    public sealed class SerializationModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<MovementBehaviorSerializer>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
