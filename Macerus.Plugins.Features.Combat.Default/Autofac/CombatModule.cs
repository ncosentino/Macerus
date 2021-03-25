using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.Combat.Default.Autofac
{
    public sealed class CombatModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<CombatCalculations>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<CombatGameObjectProvider>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}