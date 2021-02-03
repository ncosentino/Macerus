using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.Data.MySql.Autofac
{
    public sealed class MySqlModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<MySqlConnectionFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
