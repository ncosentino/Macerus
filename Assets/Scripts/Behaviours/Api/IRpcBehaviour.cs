using Assets.Scripts.Api;

namespace Assets.Scripts.Behaviours.Api
{
    public interface IRpcBehaviour
    {
        #region Properties
        IRpcClient Client { get; }
        #endregion
    }
}