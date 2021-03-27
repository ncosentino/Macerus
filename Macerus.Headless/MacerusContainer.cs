using System;
using System.Linq;
using System.Reflection;

using Autofac;

using ProjectXyz.Game.Core.Autofac;

namespace Macerus.Headless
{
    public sealed class MacerusContainer
    {
        private Lazy<ILifetimeScope> _lazyContainer = new Lazy<ILifetimeScope>(() =>
        {
            var moduleDiscoverer = new ModuleDiscoverer();
            var modules = moduleDiscoverer
                .Discover(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                .Concat(moduleDiscoverer.Discover(Assembly.GetExecutingAssembly()))
                .Where(x => x.GetType().FullName.IndexOf("Example") == -1)
                .OrderBy(x => x.GetType().FullName)
                .ToArray();
            var dependencyContainerBuilder = new DependencyContainerBuilder();
            var dependencyContainer = dependencyContainerBuilder.Create(modules);
            var lifetimeScope = dependencyContainer.BeginLifetimeScope();
            return lifetimeScope;
        });

        private ILifetimeScope Instance => _lazyContainer.Value;

        public T Resolve<T>() => Instance.Resolve<T>();
    }
}
