using Autofac;

using Macerus.Shared.Behaviors.Filtering;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Shared.Behaviors.Autofac
{
    public sealed class BehaviorModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<FilterContextAmenity>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
