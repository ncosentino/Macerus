using Assets.Scripts.Api;

namespace Assets.Scripts.Gui
{
    public interface IRpcBehaviour
    {
        #region Properties
        IRpcClient Client { get; }
        #endregion
    }
}