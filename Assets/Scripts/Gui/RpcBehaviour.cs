using Assets.Scripts.Api;
using UnityEngine;

namespace Assets.Scripts.Gui
{
    public sealed class RpcBehaviour : 
        MonoBehaviour, 
        IRpcBehaviour
    {
        #region Fields
        private IRpcContainer _rpcContainer;
        #endregion

        #region Properties
        public IRpcClient Client
        {
            get { return _rpcContainer.Client; }
        }
        #endregion

        #region Methods
        private void Start()
        {
            _rpcContainer = RpcContainer.Create();
        }
        #endregion
    }
}
