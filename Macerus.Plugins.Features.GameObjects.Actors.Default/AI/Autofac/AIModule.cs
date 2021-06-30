using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.GameObjects.Actors.Default.AI.Autofac
{
    public sealed class AIModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<AISchedulingSystem>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<WalkZoneAISystem>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<IdleAISystem>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}