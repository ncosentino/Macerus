using System.Collections.Generic;

using Autofac;

using Macerus.Plugins.Features.Resources.Default;

using ProjectXyz.Api.Framework;
using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Content.Resources
{
    public sealed class ResourcesModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(c => new InMemoryStringResourceProvider(new Dictionary<IIdentifier, string>()
                {
                    // TODO: add entries...
                }))
                .SingleInstance()
                .AsImplementedInterfaces();
        }
    }
}
