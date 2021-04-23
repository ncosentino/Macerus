using System;

using Autofac;

using Macerus.Game.Api;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Logging;
using ProjectXyz.Framework.Autofac;

namespace Macerus.Tests.Modules
{
    public sealed class GameModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<SceneManager>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<Application>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }

    public sealed class Application : IApplication
    {
        public void Exit() => Environment.Exit(0);
    }

    public sealed class SceneManager : ISceneManager
    {
        private readonly ILogger _logger;

        public SceneManager(ILogger logger)
        {
            _logger = logger;
        }

        public void GoToScene(IIdentifier sceneId)
        {
            _logger.Info($"Go to scene '{sceneId}'.");
        }
    }
}
