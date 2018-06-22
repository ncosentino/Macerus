using Autofac;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Normal
{
    public sealed class ProvidedImplementationsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<NormalItemGenerator>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
