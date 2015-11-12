using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using ProjectXyz.Api.Messaging.Interface;
using ProjectXyz.Api.Messaging.Serialization.Interface;
using RabbitMQ.Client.Events;

namespace Assets.Scripts.Api
{
    public sealed class ResponseFactory : IResponseFactory
    {
        #region Fields
        private readonly Dictionary<string, Type> _requestMapping;
        private readonly IResponseReader _responseReader;
        #endregion

        #region Constructors
        private ResponseFactory(
            IResponseReader responseReader,
            IDictionary<string, Type> requestMapping)
        {
            _responseReader = responseReader;
            _requestMapping = new Dictionary<string, Type>(requestMapping);
        }
        #endregion

        #region Methods
        public static IResponseFactory Create(
            IResponseReader responseReader,
            IDictionary<string, Type> responseMapping)
        {
            var factory = new ResponseFactory(
                responseReader,
                responseMapping);
            return factory;
        }

        public IResponse Create(BasicDeliverEventArgs deliverEventArgs)
        {
            if (!deliverEventArgs.BasicProperties.Headers.ContainsKey("Type"))
            {
                throw new InvalidOperationException("The delivered message does not contain a 'Type' header.");
            }

            var typeName = Convert.ToString(
                Encoding.UTF8.GetString(deliverEventArgs.BasicProperties.Headers["Type"] as byte[]),
                CultureInfo.InvariantCulture);

            if (!_requestMapping.ContainsKey(typeName))
            {
                throw new NotSupportedException(string.Format("Mapping of response type '{0}' is not supported.", typeName));
            }

            IResponse response;
            using (var bodyStream = new MemoryStream(deliverEventArgs.Body))
            {
                response = _responseReader.Read(
                    bodyStream,
                    _requestMapping[typeName]);
            }

            return response;
        }
        #endregion
    }
}