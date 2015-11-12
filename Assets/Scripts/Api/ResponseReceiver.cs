using System;
using ProjectXyz.Api.Messaging.Interface;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Assets.Scripts.Api
{
    public sealed class ResponseReceiver : IResponseReceiver
    {
        #region Fields
        private readonly IResponseFactory _responseFactory;
        private readonly EventingBasicConsumer _consumer;
        #endregion

        #region Constructors
        private ResponseReceiver(
            EventingBasicConsumer consumer,
            IResponseFactory responseFactory)
        {
            _responseFactory = responseFactory;

            _consumer = consumer;
            _consumer.Received += Consumer_Received;
        }

        ~ResponseReceiver()
        {
            Dispose(false);
        }
        #endregion

        #region Events
        public event EventHandler<ResponseReceivedEventArgs> ResponseReceived;
        #endregion

        #region Methods
        public static IResponseReceiver Create(
            EventingBasicConsumer consumer,
            IResponseFactory responseFactory)
        {
            var receiver = new ResponseReceiver(
                consumer,
                responseFactory);
            return receiver;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            _consumer.Received -= Consumer_Received;
        }

        private void OnResponseReceived(IResponse response)
        {
            var handler = ResponseReceived;
            if (handler != null)
            {
                handler.Invoke(this, new ResponseReceivedEventArgs(response));
            }
        }
        #endregion

        #region Event Handlers
        private void Consumer_Received(IBasicConsumer sender, BasicDeliverEventArgs args)
        {
            var response = _responseFactory.Create(args);
            OnResponseReceived(response);
        }
        #endregion
    }
}