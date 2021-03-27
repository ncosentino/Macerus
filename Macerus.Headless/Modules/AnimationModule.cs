using Autofac;

using Macerus.Plugins.Features.Animations.Lpc;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Headless
{
    public sealed class AnimationModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(c => new LpcAnimationDiscovererSettings(
                    @"..\..",
                    @"Graphics\Actors\LpcUniversal"))
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
