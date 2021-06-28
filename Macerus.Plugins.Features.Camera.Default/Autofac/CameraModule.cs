using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.Camera.Default.Autofac
{
    public sealed class CameraModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
               .RegisterType<CameraManager>()
               .AsImplementedInterfaces()
               .SingleInstance();
        }
    }
}