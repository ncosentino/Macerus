using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.Combat.Default.Autofac
{
    public sealed class CombatModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
               .RegisterType<CombatSystem>()
               .AsImplementedInterfaces()
               .SingleInstance();
            builder
               .RegisterType<CombatAISystem>()
               .AsImplementedInterfaces()
               .SingleInstance();
            builder
                .RegisterType<CombatAIFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<CombatCalculations>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<CombatGameObjectProvider>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
               .RegisterType<CombatTeamGeneratorComponentToStatsConverter>()
               .AsImplementedInterfaces()
               .SingleInstance();
        }
    }
}