﻿using Autofac;

using Macerus.Plugins.Features.GameObjects.Items.Generation.Magic.MySql;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Magic
{
    public sealed class MagicItemGenerationModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<MagicItemGenerator>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<MagicItemNameGenerator>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<MagicAffixRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<MagicItemNameGeneratorComponentToBehaviorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
