using ProjectXyz.Api.Messaging.Interface;
using RabbitMQ.Client.Events;

namespace Assets.Scripts.Api
{
    public interface IResponseFactory
    {
        #region Methods
        IResponse Create(BasicDeliverEventArgs deliverEventArgs);
        #endregion
    }
}