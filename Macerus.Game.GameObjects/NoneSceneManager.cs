using System;

using Macerus.Game.Api.Scenes;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Logging;

namespace Macerus.Game
{
    public sealed class NoneSceneManager : ISceneManager
    {
        private readonly ILogger _logger;

        public NoneSceneManager(ILogger logger)
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
