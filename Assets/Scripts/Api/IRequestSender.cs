using ProjectXyz.Api.Messaging.Interface;

namespace Assets.Scripts.Api
{
    public interface IRequestSender
    {
        #region Methods
        void Send<TRequest>(TRequest request)
            where TRequest : IRequest;
        #endregion
    }
}