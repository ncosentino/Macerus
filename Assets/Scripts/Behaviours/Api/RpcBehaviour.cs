using Assets.Scripts.Api;
using UnityEngine;

namespace Assets.Scripts.Behaviours.Api
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
            get
            {
                if (_rpcContainer == null)
                {
                    _rpcContainer = RpcContainer.Create();
                }

                return _rpcContainer.Client;
            }
        }
        #endregion
    }
}
