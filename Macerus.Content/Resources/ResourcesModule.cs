using System;
using System.Collections.Generic;

using Autofac;

using Macerus.Plugins.Features.Resources.Default;

using ProjectXyz.Api.Framework;
using ProjectXyz.Framework.Autofac;

namespace Macerus.Content.Resources
{
    public sealed class ResourcesModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(c => new InMemoryStringResourceRepository(new Dictionary<IIdentifier, string>()
                {
                    // TODO: add entries...
                }))
                .SingleInstance()
                .AsImplementedInterfaces();
            builder
                .Register(c => new FileSystemImageResourceRepository(AppDomain.CurrentDomain.BaseDirectory))
                .SingleInstance()
                .AsImplementedInterfaces();
        }
    }
}
