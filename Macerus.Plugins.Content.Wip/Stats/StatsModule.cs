using Autofac;
using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Content.Wip.Stats
{
    public sealed class StatsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<StatDefinitionIdToBoundsMappingRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}