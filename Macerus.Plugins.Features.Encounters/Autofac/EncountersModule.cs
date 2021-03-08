using Autofac;

using Macerus.Plugins.Features.Encounters.GamObjects.Static.Triggers;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.Encounters.Autofac
{
    public sealed class EncountersModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<EncounterBehaviorsProvider>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}