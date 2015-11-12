using System;
using System.Threading;
using Newtonsoft.Json;
using ProjectXyz.Api.Messaging.Interface;
using UnityEngine;

namespace Assets.Scripts.Api
{
    public sealed class RpcClient : IRpcClient
    {
        #region Fields
        private readonly IResponseReceiver _responseReceiver;
        private readonly IRequestSender _requestSender;
        #endregion

        #region Constructors
        private RpcClient(
            IRequestSender requestSender,
            IResponseReceiver responseReceiver)
        {
            _requestSender = requestSender;
            _responseReceiver = responseReceiver;
        }
        #endregion

        #region Methods
        public static IRpcClient Create(
            IRequestSender requestSender,
            IResponseReceiver responseReceiver)
        {
            var client = new RpcClient(
                requestSender,
                responseReceiver);
            return client;
        }

        public TResponse Send<TRequest, TResponse>(TRequest request, TimeSpan timeout)
            where TRequest : IRequest
            where TResponse : IResponse
        {
            TResponse response = default(TResponse);
            var trigger = new ManualResetEvent(false);

            EventHandler<ResponseReceivedEventArgs> handler = (_, e) =>
            {
                if (e.Response.RequestId != request.Id)
                {
                    return;
                }

                if (!(e.Response is TResponse))
                {
                    throw new InvalidOperationException(string.Format(
                        "The returned message type was '{0}' and the expected return type was '{1}'.", 
                        e.Response.GetType(), 
                        typeof(TResponse)));    
                }

                response = (TResponse)e.Response;
                trigger.Set();
            };

            try
            {
                _responseReceiver.ResponseReceived += handler;
                _requestSender.Send(request);
                Debug.Log("Sent request to server: " + JsonConvert.SerializeObject(request));

                if (!trigger.WaitOne(timeout))
                {
                    throw new TimeoutException(string.Format("No response was received within {0} miliseconds.", timeout.TotalMilliseconds));
                }
            }
            finally 
            {
                _responseReceiver.ResponseReceived -= handler;
            }

            Debug.Log("Got server response: " + JsonConvert.SerializeObject(response));
            return response;
        }
        #endregion
    }
}