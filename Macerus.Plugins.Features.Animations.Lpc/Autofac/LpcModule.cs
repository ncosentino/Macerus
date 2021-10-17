using Autofac;

using Macerus.Plugins.Features.Animations.Api;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.Animations.Lpc.Autofac
{
    public sealed class LpcModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<LpcAnimationDiscoverer>()
                .AutoActivate()
                .AsSelf()
                .OnActivated(x =>
                {
                    var registrar = x.Context.Resolve<ISpriteAnimationRegistrar>();
                    foreach (var sprite in x.Instance.CreateAnimations())
                    {
                        registrar.Register(sprite);
                    }
                });
            builder
                .RegisterType<NoneLpcAnimationDiscovererSettings>()
                .AsImplementedInterfaces()
                .IfNotRegistered(typeof(ILpcAnimationDiscovererSettings))
                .SingleInstance();
            builder
                .RegisterType<LpcSheetAnimationFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
