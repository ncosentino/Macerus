using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.Gui.Default.SceneTransitions.Autofac
{
    public sealed class SceneTransitionsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<TransitionController>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<FaderSceneTransitionViewModel>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}