using System;

using ProjectXyz.Api.Framework;

namespace Macerus.Game.Api.Scenes
{
    public interface ISceneManager
    {
        event EventHandler<EventArgs> SceneChanged;

        string CurrentSceneName { get; }

        void GoToScene(IIdentifier sceneId);
    }
}
