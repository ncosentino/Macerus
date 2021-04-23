using ProjectXyz.Api.Framework;

namespace Macerus.Game.Api
{
    public interface ISceneManager
    {
        void GoToScene(IIdentifier sceneId);
    }
}
