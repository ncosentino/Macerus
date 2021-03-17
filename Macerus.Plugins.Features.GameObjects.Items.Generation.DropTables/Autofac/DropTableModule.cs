using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.DropTables
{
    public sealed class DropTableModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<DropTableIdentifiers>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
