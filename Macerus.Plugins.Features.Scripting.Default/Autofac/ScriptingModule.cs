using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.Scripting.Default.Autofac
{
    public sealed class ScriptingModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
#if !NET5_0_OR_GREATER
            builder
                .RegisterType<CSharpCodeProviderScriptCompiler>()
                .AsImplementedInterfaces()
                .SingleInstance();
#else
            builder
                .RegisterType<RoslynScriptCompiler>()
                .AsImplementedInterfaces()
                .SingleInstance();
#endif
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