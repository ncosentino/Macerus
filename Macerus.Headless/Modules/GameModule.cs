using System;

using Autofac;

using Macerus.Game.Api;
using Macerus.Game.Api.Scenes;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Logging;
using ProjectXyz.Framework.Autofac;

namespace Macerus.Headless
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

        public string CurrentSceneName => null;

        public event EventHandler<EventArgs> SceneChanged;

        public void BeginNavigateToScene(
            IIdentifier sceneId,
            Action<ISceneCompletion> completedCallback)
        {
            _logger.Info($"Navigating to scene '{sceneId}'...");
        }

        public void NavigateToScene(IIdentifier sceneId)
        {
            _logger.Info($"Navigate to scene '{sceneId}'.");
        }
    }
}
