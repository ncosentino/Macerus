using System;
using Assets.Scripts.Gui;

namespace Assets.Scripts.Api
{
    public interface IResponseReceiver : IDisposable
    {
        #region Events
        event EventHandler<ResponseReceivedEventArgs> ResponseReceived;
        #endregion
    }
}