using Autofac;

using Macerus.Plugins.Features.GameObjects.Items.Affixes.MySql;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.GameObjects.Items.Affixes.Autofac
{
    public sealed class AffixesModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<AffixTypeRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}