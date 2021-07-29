using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.Audio.SoundGeneration.Default.Autofac
{
    public sealed class SoundGenerationModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<SoundGenerator>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
