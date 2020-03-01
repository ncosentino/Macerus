using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.GameObjects.Enchantments.Generation.Magic
{
    public sealed class ProvidedImplementationsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<MagicEnchantmentGenerator>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
