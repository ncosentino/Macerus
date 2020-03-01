using Autofac;
using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Magic
{
    public sealed class ProvidedImplementationsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<MagicItemGenerator>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
