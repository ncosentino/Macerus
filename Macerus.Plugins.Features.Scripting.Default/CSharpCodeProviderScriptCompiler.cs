﻿#if !NET5_0_OR_GREATER
using System;
using System.CodeDom.Compiler;
using System.Linq;
using System.Threading.Tasks;

using Autofac;

using Microsoft.CSharp;

namespace Macerus.Plugins.Features.Scripting.Default
{
    public sealed class CSharpCodeProviderScriptCompiler
        : IScriptCompiler
    {
        private readonly CSharpCodeProvider _provider;
        private readonly ILifetimeScope _lifetimeScope;
        private readonly IScriptReferenceCalculator _scriptReferenceCalculator;

        public CSharpCodeProviderScriptCompiler(
            ILifetimeScope lifetimeScope,
            IScriptReferenceCalculator scriptReferenceCalculator)
        {
            _provider = new CSharpCodeProvider();
            _lifetimeScope = lifetimeScope;
            _scriptReferenceCalculator = scriptReferenceCalculator;
        }

        public async Task<ICompiledScript> CompileFromRawAsync(
            string rawScript,
            string fullClassName,
            bool singleInstance)
        {
            var script = await Task
                .Run(() => CompileFromRawTaskBody(
                    rawScript,
                    fullClassName,
                    singleInstance))
                .ConfigureAwait(false);
            return script;
        }

        private ICompiledScript CompileFromRawTaskBody(
            string rawScript,
            string fullClassName,
            bool singleInstance)
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

            var createScriptMethod = new Func<IScript>(() => constructorWithDependencyInjection == null
                ? (IScript)Activator.CreateInstance(scriptType)
                : (IScript)constructorWithDependencyInjection.Invoke(new[] { constructorParametersInstance }));
            var scriptInstance = singleInstance
                ? (ICompiledScript)new SingleInstanceScript(
                    createScriptMethod.Invoke(),
                    fullClassName)
                : (ICompiledScript)new NewInstanceScript(
                    createScriptMethod,
                    fullClassName);
            return scriptInstance;
        }
    }
}
#endif