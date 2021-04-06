using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.Mapping.Autofac
{
    public sealed class MappingModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<BoundingRectangleCollisionDetector>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
