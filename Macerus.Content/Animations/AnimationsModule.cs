﻿using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Content.Animations
{
    public sealed class AnimationsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<SpriteAnimationRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
