using Autofac;

using Macerus.Plugins.Features.Animations.Lpc;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Tests.Modules
{
    public sealed class AnimationModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(c => new LpcAnimationDiscovererSettings(
                    @"..\..\..\..\Macerus.Plugins.Content",
                    @"Graphics\Actors\LpcUniversal"))
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
