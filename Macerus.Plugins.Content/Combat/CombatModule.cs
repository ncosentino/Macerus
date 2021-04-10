using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Content.Combat
{
    public sealed class CombatModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<CombatTeamIdentifiers>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<CombatStatIdentifiers>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
