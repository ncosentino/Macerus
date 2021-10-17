using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Content.Affixes
{
    public sealed class AffixesModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<AffixTemplate>()
                .SingleInstance();      
        }
    }
}
