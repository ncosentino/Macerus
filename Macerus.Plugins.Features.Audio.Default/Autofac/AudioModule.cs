using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.Audio.Default.Autofac
{
    public sealed class AudioModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<NoneAudioManager>()
                .AsImplementedInterfaces()
                .IfNotRegistered(typeof(IAudioManager))
                .SingleInstance();
        }
    }
}
