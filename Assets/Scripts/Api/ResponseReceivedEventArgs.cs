using System;
using ProjectXyz.Api.Messaging.Interface;

namespace Assets.Scripts.Api
{
    public sealed class ResponseReceivedEventArgs : EventArgs
    {
        #region Constructors
        public ResponseReceivedEventArgs(IResponse response)
        {
            Response = response;
        }
        #endregion

        #region Properties
        public IResponse Response { get; set; }
        #endregion
    }
}