using System;
using System.CodeDom.Compiler;
using System.Linq;
using System.Threading.Tasks;

using Autofac;

using Microsoft.CSharp;

namespace Macerus.Plugins.Features.Scripting.Default
{
    public sealed class ScriptCompiler : IScriptCompiler
    {
        private readonly CSharpCodeProvider _provider;
        private readonly ILifetimeScope _lifetimeScope;
        private readonly IScriptReferenceCalculator _scriptReferenceCalculator;

        public ScriptCompiler(
            ILifetimeScope lifetimeScope,
            IScriptReferenceCalculator scriptReferenceCalculator)
        {
            _provider = new CSharpCodeProvider();
            _lifetimeScope = lifetimeScope;
            _scriptReferenceCalculator = scriptReferenceCalculator;
        }

        public async Task<IScript> CompileFromRawAsync(
            string rawScript,
            string fullClassName)
        {
            var script = await Task
                .Run(() => CompileFromRawTaskBody(rawScript, fullClassName))
                .ConfigureAwait(false);
            return script;
        }

        private IScript CompileFromRawTaskBody(
            string rawScript,
            string fullClassName)
        {
            var parameters = new CompilerParameters();

            foreach (var referencePath in _scriptReferenceCalculator.GetScriptReferencePaths(rawScript))
            {
                parameters.ReferencedAssemblies.Add(referencePath);
            }

            parameters.GenerateInMemory = true;
            parameters.GenerateExecutable = false;

            var compilationResults = _provider.CompileAssemblyFromSource(
                parameters,
                rawScript);

            var assembly = compilationResults.CompiledAssembly;
            var scriptType = assembly.GetType(fullClassName);

            var constructorWithDependencyInjection = scriptType.GetConstructors().FirstOrDefault(x =>
            {
                var constructorParameters = x.GetParameters();
                return
                    constructorParameters.Length == 1 &&
                    typeof(IScriptConstructorParameters).IsAssignableFrom(constructorParameters[0].ParameterType);
            });

            var constructorParametersType = constructorWithDependencyInjection == null
                ? null
                : constructorWithDependencyInjection.GetParameters()[0].ParameterType;
            var constructorParametersInstance = constructorParametersType == null
                ? null
                : Activator.CreateInstance(constructorParametersType);

            if (constructorParametersInstance != null)
            {
                _lifetimeScope.InjectUnsetProperties(constructorParametersInstance);
            }

            var scriptInstance = constructorWithDependencyInjection == null
                ? (IScript)Activator.CreateInstance(scriptType)
                : (IScript)constructorWithDependencyInjection.Invoke(new[] { constructorParametersInstance });
            return scriptInstance;
        }
    }
}
