using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.Scripting.Default.Autofac
{
    public sealed class ScriptingModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<ScriptCompiler>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ScriptReferenceCalculator>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ScriptRepositoryFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}