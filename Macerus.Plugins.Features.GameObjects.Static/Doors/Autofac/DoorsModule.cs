using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.GameObjects.Static.Doors.Autofac
{
    public sealed class DoorsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<DoorInteractableBehaviorProvider>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<DoorInteractionHandler>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}