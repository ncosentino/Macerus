using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace Macerus.Plugins.Features.GameObjects.Enchantments.Generation.Magic
{
    public sealed class ProvidedImplementationsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<MagicEnchantmentGenerator>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
