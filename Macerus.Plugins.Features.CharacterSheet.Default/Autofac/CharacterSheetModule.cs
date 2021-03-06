﻿using Autofac;

using Macerus.Plugins.Features.CharacterSheet.Default;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.Inventory.Default.Autofac
{
    public sealed class CharacterSheetModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<CharacterSheetViewModel>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<CharacterSheetController>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}