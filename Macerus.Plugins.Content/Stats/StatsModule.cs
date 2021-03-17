using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Content.Stats
{
    public sealed class StatsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<StatDefinitionIdToBoundsMappingRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<StatDefinitionToTermMappingRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}